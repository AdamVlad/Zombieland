using UnityEngine;

using AB_Utility.FromSceneToEntityConverter;

using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;

using Assets.Game.Scripts.Controllers;
using Assets.Game.Scripts.Model.ScriptableObjects;
using Assets.Game.Scripts.Model.AppData;
using Assets.Game.Scripts.Model.Components.Events;
using Assets.Game.Scripts.Model.Systems.Player;
using Assets.Game.Scripts.Model.Components.Events.Input;
using Assets.Game.Scripts.Model.Systems.Debugs;
using Assets.Game.Scripts.Model.Systems.Input;
using Assets.Game.Scripts.Model.Systems;
using Assets.Game.Scripts.Model.Components.Requests;
using Assets.Game.Scripts.View.Systems;
using System;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsPhysics.Emitter;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsPhysics.Extensions;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents.EntityReference;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsDelay;
using Assets.Game.Scripts.Model.Components.Delayed;

#if UNITY_EDITOR
using Leopotam.EcsLite.UnityEditor;
#endif

namespace Assets.Game.Scripts
{
    [Serializable]
    internal struct DebugControls
    {
        public bool IsEcsWorldDebugEnable;
        public bool IsCollisionEnterDebugEnable;
        public bool IsTriggerEnterDebugEnable;
        public bool IsPickUpItemDebugEnable;
        public bool IsShootStartedOrCanceledDebugEnable;
        public bool IsPlayerRotationRaycastEnable;
        public bool IsShootingDirectionRaycastEnable;
    }

    internal sealed class GameEntryPoint : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private GameConfigurationSo _gameSettings; 
        [SerializeField] private SceneConfigurationSo _sceneSettings; 
        [SerializeField] private BobConfigurationSO _bobSettings;

        [Space, Header("Debugs")]
        [SerializeField] private DebugControls _debugControls;

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
                EventsBus = new EventsBus(16),
                MainCamera = Camera.main,
            };

            _initSystems = new EcsSystems(_world, _sharedData);
            _initSystems
                .Add(new PlayerInitSystem())
                .Add(new ParentHolderInitSystem())
                .Add(new EntityReferenceInitSystem())
                .Add(new InputInitSystem())
                .Add(new ScreenInitSystem())
                .Add(new VmCameraInitSystem())
                .Inject(_bobSettings)
                .Inject(_gameSettings)
                .ConvertScene()
                .Init();

            _updateSystems = new EcsSystems(_world, _sharedData);
            _updateSystems
                #region Debug Systems
#if UNITY_EDITOR
                .Add(new EcsWorldDebugSystem(), _debugControls.IsEcsWorldDebugEnable)
                .Add(new EcsSystemsDebugSystem(), _debugControls.IsEcsWorldDebugEnable)
                .Add(new CollisionEnterDebugSystem(), _debugControls.IsCollisionEnterDebugEnable)
                .Add(new TriggerEnterDebugSystem(), _debugControls.IsTriggerEnterDebugEnable)
#endif
                #endregion
                .Add(new DelayedOperationSystem<DestructionDelayed>())
                .Add(new DestructionSystem())
                .Add(new InputMoveSystem())
                .Add(new InputScreenPositionSystem())
                .Add(new InputShootSystem())
                .Add(new InputShootDirectionChangingSystem())
                .Add(new PlayerItemPickupSystem())
                .DelHerePhysics()
                .Add(new PlayerWeaponDropSystem())
                .Add(new PlayerWeaponPickupSystem())
                #region Debug Systems
#if UNITY_EDITOR
                .Add(new PickUpItemDebugSystem(), _debugControls.IsPickUpItemDebugEnable)
                .Add(new ShootStartedOrCanceledDebugSystem(), _debugControls.IsShootStartedOrCanceledDebugEnable)
                .Add(new PlayerRotationRaycastSystem(), _debugControls.IsPlayerRotationRaycastEnable)
                .Add(new ShootingDirectionRaycastSystem(), _debugControls.IsShootingDirectionRaycastEnable)
#endif
            #endregion
                .Add(GetEventsDestroySystem())
                //.DelHere<DestructionDelayed>()
                .Inject(_bobSettings)
                .Inject(_sceneSettings)
                .Init();

            _fixedUpdateSystems = new EcsSystems(_world, _sharedData);
            _fixedUpdateSystems
                .Add(new PlayerRotationSystem())
                .Add(new PlayerMoveSystem())
                .Add(new PlayerAnimatorMoveParameterRequestSystem())
                .Add(new PlayerAnimatorTakeWeaponParameterRequestSystem())
                .Add(new PlayerAnimatorShootParameterRequestSystem())
                .Add(new AnimationSystem())
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