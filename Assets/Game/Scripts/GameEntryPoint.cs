using AB_Utility.FromSceneToEntityConverter;
using Assets.Game.Scripts.Controllers;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Assets.Game.Scripts.Model.ScriptableObjects;
using UnityEngine;
using Assets.Plugins.IvaLeoEcsLite.EcsPhysics.Emitter;
using Assets.Plugins.IvaLeoEcsLite.EcsPhysics.Extensions;
using Assets.Plugins.IvaLeoEcsLite.UnityEcsComponents.EntityReference;
using Assets.Game.Scripts.Model.AppData;
using Assets.Plugins.IvaLeoEcsLite.EcsEvents;
using Assets.Game.Scripts.Model.Components.Events;
using Assets.Game.Scripts.Model.Systems.Player;
using Assets.Game.Scripts.Model.Components.Events.Input;
using Assets.Game.Scripts.Model.Systems.Debugs;
using Assets.Game.Scripts.Model.Systems.Input;
using Assets.Game.Scripts.Model.Systems;
using Leopotam.EcsLite.ExtendedSystems;
using Assets.Game.Scripts.Model.Components.Requests;

#if UNITY_EDITOR
using Leopotam.EcsLite.UnityEditor;
#endif

namespace Assets.Game.Scripts
{
    internal sealed class GameEntryPoint : MonoBehaviour
    {
        [SerializeField] private BobConfigurationSO _bobSettings; 
        [SerializeField] private LayerMask _raycastableMask; 

        private EcsWorld _world;

        private IEcsSystems _initSystems;
        private IEcsSystems _updateSystems;
        private IEcsSystems _fixedUpdateSystems;

        private SharedData _sharedData;

        private void Awake()
        {
            //Application.targetFrameRate = 60;

            _world = new EcsWorld();
            EcsPhysicsEvents.World = _world;

            _sharedData = new SharedData
            {
                EventsBus = new EventsBus(),
                MainCamera = Camera.main,
                RaycastableMask = _raycastableMask
            };

            _initSystems = new EcsSystems(_world, _sharedData);
            _initSystems
                .Add(new PlayerInitSystem())
                .Add(new EntityReferenceInitSystem())
                .Add(new InputInitSystem())
                .Add(new ScreenInitSystem())
                .Inject(_bobSettings)
                .ConvertScene()
                .Init();

            _updateSystems = new EcsSystems(_world, _sharedData);
            _updateSystems
                #region Debug Systems
#if UNITY_EDITOR
                .Add(new EcsWorldDebugSystem())
                .Add(new EcsSystemsDebugSystem())
                .Add(new CollisionEnterDebugSystem())
                .Add(new TriggerEnterDebugSystem())
#endif
                #endregion
                .Add(new InputMoveSystem())
                .Add(new InputScreenPositionSystem())
                .Add(new InputShootSystem())
                .Add(new InputShootDirectionChangingSystem())
                .Add(new PlayerItemPickupSystem())
                .DelHerePhysics()
                .Add(new PlayerWeaponPickupSystem())
                #region Debug Systems
#if UNITY_EDITOR
                .Add(new PickUpItemDebugSystem())
                .Add(new ShootStartedOrCanceledDebugSystem())
                .Add(new PlayerRotationRaycastSystem())
                .Add(new ShootingDirectionRaycastSystem())
#endif
                #endregion

                .Add(GetEventsDestroySystem())
                .Inject(_bobSettings)
                .Init();

            _fixedUpdateSystems = new EcsSystems(_world);
            _fixedUpdateSystems
                .Add(new PlayerRotationSystem())
                .Add(new PlayerMoveSystem())
                .Add(new PlayerAnimatorMoveParameterRequestSystem())
                .Add(new PlayerAnimatorTakeWeaponParameterRequestSystem())
                .Add(new PlayerAnimationSystem())
                .DelHere<SetAnimatorParameterRequests>()
                .Inject(_bobSettings)
                .Init();
        }

        private void Update()
        {
            _updateSystems?.Run();
        }

        private void FixedUpdate()
        {
            _fixedUpdateSystems?.Run();
        }

        private IEcsSystem GetEventsDestroySystem()
        {
            return new DestroyEventsSystem(_sharedData.EventsBus, 16)
                .IncSingleton<InputMoveChangedEvent>()
                .IncSingleton<InputShootStartedEvent>()
                .IncSingleton<InputShootEndedEvent>()
                .IncSingleton<InputShootDirectionChangedEvent>()
                .IncSingleton<PlayerPickUpWeaponEvent>();
        }

        private void OnDestroy()
        {
            _initSystems?.Destroy();
            _initSystems = null;
            
            _updateSystems?.Destroy();
            _updateSystems = null;
            
            EcsPhysicsEvents.World = null;
            _fixedUpdateSystems?.Destroy();
            _fixedUpdateSystems = null;

            _world?.Destroy();
            _world = null;

            _sharedData.EventsBus.Destroy();
        }
    }
}