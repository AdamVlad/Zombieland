#region References

using System;
using AB_Utility.FromSceneToEntityConverter;
using Assets.Game.Scripts.Levels.Controllers;
using Assets.Game.Scripts.Levels.Model.Components.Delayed;
using Assets.Game.Scripts.Levels.Model.Components.Enemies;
using Assets.Game.Scripts.Levels.Model.Components.Events;
using Assets.Game.Scripts.Levels.Model.Components.Events.Input;
using Assets.Game.Scripts.Levels.Model.Components.Events.Shoot;
using Assets.Game.Scripts.Levels.Model.Components.Requests;
using Assets.Game.Scripts.Levels.Model.Factories;
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
using Assets.Game.Scripts.Levels.Model.Extensions;
using Assets.Game.Scripts.Levels.Model.Systems.Enemies;
using Zenject;

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
        [Space, Header("Enemies")]
        [SerializeField] private Enemy _enemy;
        [SerializeField] private Transform _enemyInitialPosition;

        [Space, Header("Debugs")]
        [SerializeField] private DebugControls _debugControls;

        [Inject] private WeaponsProviderService _weaponsProviderService;
        [Inject] private ChargesProviderService _chargesProviderService;

        [Inject] private EcsWorld _world;
        [Inject] private EnemyFactory _enemyFactory;
        [Inject] private EventsBus _eventsBus;
        [Inject] private DiContainer _container;

        private IEcsSystems _initSystems;
        private IEcsSystems _updateSystems;
        private IEcsSystems _fixedUpdateSystems;

        private void Awake()
        {
            //Application.targetFrameRate = 60;

            EcsPhysicsEvents.World = _world;

            _weaponsProviderService.Run();
            _chargesProviderService.Run();

            _enemyFactory.Create(_enemy, _enemyInitialPosition.position);
            _enemyFactory.Create(_enemy, _enemyInitialPosition.position);

            _initSystems = new EcsSystems(_world);
            _initSystems
                .Add<WeaponInitSystem>(_container)
                .Add<WeaponSpawnerInitSystem>(_container)
                .Add<PlayerInitSystem>(_container)
                .Add<EntityReferenceInitSystem>(_container)
                .Add<ParentHolderInitSystem>(_container)
                .Add<InputInitSystem>(_container)
                .Add<ScreenInitSystem>(_container)
                .Add<VmCameraInitSystem>(_container)
                .Inject()
                .ConvertScene()
                .Init();

            _updateSystems = new EcsSystems(_world);
            _updateSystems
                #region Debug Systems
#if UNITY_EDITOR
                .Add<EcsWorldDebugSystem>(_container, _debugControls.IsEcsWorldDebugEnable)
                .Add<EcsSystemsDebugSystem>(_container, _debugControls.IsEcsWorldDebugEnable)
                .Add<CollisionEnterDebugSystem>(_container, _debugControls.IsCollisionEnterDebugEnable)
                .Add<TriggerEnterDebugSystem>(_container, _debugControls.IsTriggerEnterDebugEnable)
#endif
                #endregion
                .Add<DelayedAddOperationSystem<DestructionDelayed>>(_container)
                .Add<DelayedAddOperationSystem<WeaponSpawnDelayed>>(_container)
                .Add<DelayedRemoveOperationSystem<AttackDelayed>>(_container)
                .Add<DelayedRemoveOperationSystem<ReloadingDelayed>>(_container)
                .Add<WeaponsDestructionSystem>(_container)
                .Add<DestructionSystem>(_container)
                .Add<InputMoveSystem>(_container)
                .Add<InputOnScreenPositionChangingSystem>(_container)
                .Add<InputShootSystem>(_container)
                .Add<InputShootDirectionChangingSystem>(_container)
                .Add<PlayerItemPickupSystem>(_container)
                .Add<PlayerAttackSystem>(_container)
                .Add<PlayerReloadingSystem>(_container)
                .Add<ChargesGetFromPoolSystem>(_container)
                .Add<ChargesMoveSystem>(_container)
                .Add<ChargesLifetimeSystem>(_container)
                .Add<ChargesCollisionsSystem>(_container)
                .Add<ChargesReturnToPoolSystem>(_container)
                .DelHerePhysics()
                .Add<PlayerWeaponDropSystem>(_container)
                .Add<PlayerReloadingBreakSystem>(_container)
                .Add<PlayerWeaponPickupSystem>(_container)
                .Add<WeaponSetSpawnTimeSystem>(_container)
                .Add<WeaponSpawnSystem>(_container)
                .Add<WeaponAnimationSystem>(_container)
                .Add<EnemiesEvaluateSystem>(_container)
                .Add<EnemiesBehaveSystem>(_container)
                .Add<GetDamageSystem>(_container)
                .Add<EnemyHpBarShowingSystem>(_container)
                .Add<EnemyHpBarLifetimeSystem>(_container)
                .Add<EnemyHpBarHidingSystem>(_container)
                .Add<EnemyHpBarLookAtSystem>(_container)
                .DelHere<WeaponAnimationStartRequest>()
                .DelHere<WeaponAnimationStopRequest>()
            #region Debug Systems
#if UNITY_EDITOR
                .Add<PickUpItemDebugSystem>(_container, _debugControls.IsPickUpItemDebugEnable)
                .Add<ShootStartedOrCanceledDebugSystem>(_container, _debugControls.IsShootStartedOrCanceledDebugEnable)
                .Add<PlayerRotationRaycastSystem>(_container, _debugControls.IsPlayerRotationRaycastEnable)
                .Add<ShootingDirectionRaycastSystem>(_container, _debugControls.IsShootingDirectionRaycastEnable)
#endif
            #endregion
                .Add(GetEventsDestroySystem())
                .Inject()
                .Init();

            _fixedUpdateSystems = new EcsSystems(_world);
            _fixedUpdateSystems
                .Add<PlayerRotationSystem>(_container)
                .Add<PlayerMoveSystem>(_container)
                .Add<PlayerAnimatorMoveParameterRequestSystem>(_container)
                .Add<PlayerAnimatorTakeWeaponParameterRequestSystem>(_container)
                .Add<PlayerAnimatorShootParameterRequestSystem>(_container)
                .Add<EnemiesAnimatorMoveParameterRequestSystem>(_container)
                .Add<EnemiesAnimatorAttackParameterRequestSystem>(_container)
                .Add<AnimationSystem>(_container)
                .DelHere<SetAnimatorParameterRequests>()
                .Inject()
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
            return new DestroyEventsSystem(_eventsBus, 16)
                .IncSingleton<InputMoveChangedEvent>()
                .IncSingleton<InputOnScreenStartedEvent>()
                .IncSingleton<InputOnScreenEndedEvent>()
                .IncSingleton<InputOnScreenPositionChangedEvent>()
                .IncSingleton<ShootStartedEvent>()
                .IncSingleton<ShootEndedEvent>()
                .IncSingleton<PlayerPickUpWeaponEvent>()
                .IncSingleton<PlayerReloadingEvent>()
                .IncReplicant<GetDamageEvent>()
                .IncReplicant<HideEvent>()
                .IncReplicant<ChargeGetFromPoolEvent>()
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

            _eventsBus.Destroy();
        }
    }
}