#region References

using System;
using AB_Utility.FromSceneToEntityConverter;
using Assets.Game.Scripts.Levels.Controllers;
using Assets.Game.Scripts.Levels.Model.AppData;
using Assets.Game.Scripts.Levels.Model.Components.Delayed;
using Assets.Game.Scripts.Levels.Model.Components.Events;
using Assets.Game.Scripts.Levels.Model.Components.Events.Input;
using Assets.Game.Scripts.Levels.Model.Components.Events.Shoot;
using Assets.Game.Scripts.Levels.Model.Components.Requests;
using Assets.Game.Scripts.Levels.Model.Components.Weapons.Charges;
using Assets.Game.Scripts.Levels.Model.Creators;
using Assets.Game.Scripts.Levels.Model.Factories;
using Assets.Game.Scripts.Levels.Model.Repositories;
using Assets.Game.Scripts.Levels.Model.ScriptableObjects;
using Assets.Game.Scripts.Levels.Model.Services;
using Assets.Game.Scripts.Levels.Model.Systems;
using Assets.Game.Scripts.Levels.Model.Systems.Charges;
using Assets.Game.Scripts.Levels.Model.Systems.Debugs;
using Assets.Game.Scripts.Levels.Model.Systems.Input;
using Assets.Game.Scripts.Levels.Model.Systems.Player;
using Assets.Game.Scripts.Levels.Model.Systems.Weapons;
using Assets.Game.Scripts.Levels.View.Systems;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsDelay;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsPhysics.Emitter;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsPhysics.Extensions;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents.EntityReference;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;
using UnityEngine;
using Assets.Game.Scripts.Levels.Model.Components.Events.Charges;

#if UNITY_EDITOR
using Leopotam.EcsLite.UnityEditor;
#endif

#endregion

namespace Assets.Game.Scripts.Levels
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

    // TODO:
    // Убрать MonoLink<Weapon> и сделать отдельный компонент для Weapon - WeaponComponent
    // Разбить код на методы во входной точке

    internal sealed class GameEntryPoint : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private GameConfigurationSo _gameSettings; 
        [SerializeField] private SceneConfigurationSo _sceneSettings; 
        [SerializeField] private PlayerConfigurationSo _playerSettings;

        [Space, Header("Weapons")]
        [SerializeField] private GameObject[] _weapons;
        [SerializeField] private Transform _weaponsInitialParent;

        [Space, Header("Bullets")]
        [SerializeField] private Charge[] _charges;
        [SerializeField] private Transform _chargesInitialParent;

        [Space, Header("Debugs")]
        [SerializeField] private DebugControls _debugControls;

        private WeaponsProviderService _weaponsProviderService;
        private ChargesProviderService _chargesProviderService;

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

            var weaponsRepository = new WeaponsRepository(_weapons);
            var weaponFactory = new WeaponFactory(_weaponsInitialParent);
            var weaponsCreator = new WeaponsCreator(
                weaponsRepository,
                weaponFactory,
                _world);

            _weaponsProviderService = new WeaponsProviderService(weaponsCreator);
            _weaponsProviderService.Run();

            var chargesRepository = new ChargesRepository(_charges);
            var chargesFactory = new ChargesFactory(_chargesInitialParent);

            _chargesProviderService = new ChargesProviderService(
                chargesRepository,
                chargesFactory,
                _world);
            _chargesProviderService.Run();

            _initSystems = new EcsSystems(_world, _sharedData);
            _initSystems
                .Add(new WeaponInitSystem())
                .Add(new WeaponSpawnerInitSystem())
                .Add(new PlayerInitSystem())
                .Add(new EntityReferenceInitSystem())
                .Add(new ParentHolderInitSystem())
                .Add(new InputInitSystem())
                .Add(new ScreenInitSystem())
                .Add(new VmCameraInitSystem())
                .Inject(_playerSettings)
                .Inject(_gameSettings)
                .Inject(_weaponsProviderService)
                .Inject(_chargesProviderService)
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
                .Add(new DelayedAddOperationSystem<DestructionDelayed>())
                .Add(new DelayedAddOperationSystem<WeaponSpawnDelayed>())
                .Add(new DelayedRemoveOperationSystem<ShootingDelayed>())
                .Add(new DelayedRemoveOperationSystem<ReloadingDelayed>())
                .Add(new WeaponsDestructionSystem())
                .Add(new DestructionSystem())
                .Add(new InputMoveSystem())
                .Add(new InputOnScreenPositionChangingSystem())
                .Add(new InputShootSystem())
                .Add(new InputShootDirectionChangingSystem())
                .Add(new PlayerItemPickupSystem())
                .Add(new PlayerAttackSystem())
                .Add(new PlayerReloadingSystem())
                .Add(new ChargesCreateSystem())
                .Add(new ChargesMoveSystem())
                .Add(new ChargesLifetimeSystem())
                .Add(new ChargesCollisionsSystem())
                .Add(new ChargesReturnToPoolSystem())
                .DelHerePhysics()
                .Add(new PlayerWeaponDropSystem())
                .Add(new PlayerReloadingBreakSystem())
                .Add(new PlayerWeaponPickupSystem())
                .Add(new WeaponSetSpawnTimeSystem())
                .Add(new WeaponSpawnSystem())
                .Add(new WeaponAnimationSystem())
                .DelHere<WeaponAnimationStartRequest>()
                .DelHere<WeaponAnimationStopRequest>()
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
                .Inject(_playerSettings)
                .Inject(_sceneSettings)
                .Inject(_weaponsProviderService)
                .Inject(_chargesProviderService)
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
                .Inject(_playerSettings)
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
                .IncSingleton<InputOnScreenStartedEvent>()
                .IncSingleton<InputOnScreenEndedEvent>()
                .IncSingleton<InputOnScreenPositionChangedEvent>()
                .IncSingleton<ShootStartedEvent>()
                .IncSingleton<ShootEndedEvent>()
                .IncSingleton<PlayerPickUpWeaponEvent>()
                .IncSingleton<PlayerReloadingEvent>()
                .IncReplicant<ChargeCreatedEvent>()
                .IncReplicant<ChargeReturnToPoolEvent>();
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