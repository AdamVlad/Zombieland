using Assets.Game.Scripts.Levels.Controllers;
using Assets.Game.Scripts.Levels.Model.Components.Data.Delayed;
using Assets.Game.Scripts.Levels.Model.Components.Data.Enemies;
using Assets.Game.Scripts.Levels.Model.Components.Data.Events;
using Assets.Game.Scripts.Levels.Model.Components.Data.Events.Charges;
using Assets.Game.Scripts.Levels.Model.Components.Data.Events.Input;
using Assets.Game.Scripts.Levels.Model.Components.Data.Events.Shoot;
using Assets.Game.Scripts.Levels.Model.Components.Data.Requests;
using Assets.Game.Scripts.Levels.Model.Systems;
using Assets.Game.Scripts.Levels.Model.Systems.Charges;
using Assets.Game.Scripts.Levels.Model.Systems.Input;
using Assets.Game.Scripts.Levels.Model.Systems.Player;
using Assets.Game.Scripts.Levels.View.Systems;
using Assets.Game.Scripts.Levels.Model.Components.Data.Processes;
using Assets.Game.Scripts.Levels.Model.Practices.Extensions;
using Assets.Game.Scripts.Levels.Model.Practices.Factories;
using Assets.Game.Scripts.Levels.Model.Systems.Enemies;
using Assets.Game.Scripts.Levels.Model.Components.Data.Player;
using Assets.Game.Scripts.Levels.View.Widgets;
using Assets.Game.Scripts.Levels.Model.Components.Data.Weapons;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsDelay;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsPhysics.Emitter;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsPhysics.Extensions;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents.EntityReference;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsProcess;

using UnityEngine;
using AB_Utility.FromSceneToEntityConverter;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;
using Zenject;

#if UNITY_EDITOR
using Leopotam.EcsLite.UnityEditor;
#endif

namespace Assets.Game.Scripts.Levels
{
    // TODO:
    // ������ MonoLink<RangedWeapon> � ������� ��������� ��������� ��� RangedWeapon - WeaponComponent
    // ������� ��� �� ������ �� ������� �����

    internal sealed class GameEntryPoint : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;
        [SerializeField] private Transform _enemyInitialPosition;

        [SerializeField] private Player _player;

        [Inject] private EcsWorld _world;
        [Inject] private EnemyFactory _enemyFactory;
        [Inject] private PlayerFactory _playerFactory;
        [Inject] private Plugins.IvaLib.UnityLib.Factory.IFactory<RangedWeapon, RangedWeapon> _weaponFactory;
        [Inject] private EventsBus _eventsBus;
        [Inject] private DiContainer _container;

        private IEcsSystems _initSystems;
        private IEcsSystems _updateSystems;
        private IEcsSystems _fixedUpdateSystems;

        private void Awake()
        {
            //Application.targetFrameRate = 60;

            EcsPhysicsEvents.World = _world;

            var player = _playerFactory.Create(_player);
            var weapon = _weaponFactory.Create(player.Settings.WeaponPrefab);
            weapon.transform.parent = player.WeaponHolderPoint;
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;

            //������� ��� �� �������� ������
            for (int i = 0; i < 20; i++)
            {
                _enemyFactory.Create(_enemy, _enemyInitialPosition.position);
            }

            _initSystems = new EcsSystems(_world);
            _initSystems
                .Add<EntityReferenceInitSystem>(_container)
                .Add<InputInitSystem>(_container)
                .Add<ScreenInitSystem>(_container)
                .Add<VmCameraInitSystem>(_container)
                .Add<HudsViewInitSystem>(_container)
                .Inject()
                .ConvertScene()
                .Init();

            _updateSystems = new EcsSystems(_world);
            _updateSystems

#if UNITY_EDITOR
                .Add<EcsWorldDebugSystem>(_container)
                .Add<EcsSystemsDebugSystem>(_container)
#endif

                .Add<ProcessSystem<HpBarActiveProcess>>(_container)
                .Add<ProcessSystem<ChargeActiveProcess>>(_container)

                .Add<DelayedAddOperationSystem<DestructionDelayed>>(_container)
                .Add<DelayedRemoveOperationSystem<AttackDelayed>>(_container)
                .Add<DelayedRemoveOperationSystem<ReloadingDelayed>>(_container)

                .Add<DestructionSystem>(_container)
                .Add<InputMoveSystem>(_container)
                .Add<InputOnScreenPositionChangingSystem>(_container)
                .Add<InputShootSystem>(_container)
                .Add<InputShootDirectionChangingSystem>(_container)
                .Add<PlayerAttackSystem>(_container)
                .Add<PlayerReloadingSystem>(_container)
                .Add<ChargesGetFromPoolSystem>(_container)
                .Add<ChargesMoveSystem>(_container)
                .Add<ChargesCollisionsSystem>(_container)
                .Add<ChargesReturnToPoolSystem>(_container)
                .Add<ChargesCurrentCountWidgetRequestsSystem>(_container)

                .DelHerePhysics()

                .Add<EnemiesEvaluateSystem>(_container)
                .Add<EnemiesBehaveSystem>(_container)
                .Add<EnemyHpBarActivateSystem>(_container)
                .Add<EnemyHpBarDeactivateSystem>(_container)
                .Add<GetDamageSystem>(_container)
                .Add<PlayerHpWidgetRequestSystem>(_container)
                .Add<EnemyHpWidgetsUpdateRequestSystem>(_container)

                .Add<UpdateWidgetSystem<PlayerHpWidget, float>>(_container)
                .DelHere<UpdateWidgetRequest<PlayerHpWidget, float>>()
                .Add<UpdateWidgetSystem<EnemyHpWidget, float>>(_container)
                .DelHere<UpdateWidgetRequest<EnemyHpWidget, float>>()
                .Add<UpdateWidgetSystem<WeaponIconWidget, Sprite>>(_container)
                .DelHere<UpdateWidgetRequest<WeaponIconWidget, Sprite>>()
                .Add<UpdateWidgetSystem<CurrentChargesCountWidget, int>>(_container)
                .DelHere<UpdateWidgetRequest<CurrentChargesCountWidget, int>>()
                .Add<UpdateWidgetSystem<TotalChargesCountWidget, int>>(_container)
                .DelHere<UpdateWidgetRequest<TotalChargesCountWidget, int>>()

                .Add(GetEventsDestroySystem())
                .Inject()
                .Init();

            _fixedUpdateSystems = new EcsSystems(_world);
            _fixedUpdateSystems
                .Add<PlayerRotationSystem>(_container)
                .Add<PlayerMoveSystem>(_container)
                .Add<PlayerAnimatorMoveParameterRequestSystem>(_container)
                .Add<PlayerAnimatorShootParameterRequestSystem>(_container)
                .Add<EnemiesAnimatorMoveParameterRequestSystem>(_container)
                .Add<EnemiesAnimatorAttackParameterRequestSystem>(_container)
                .Add<EnemyHpBarLookAtSystem>(_container)
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
                .IncSingleton<PlayerReloadingEvent>()
                .IncReplicant<GetDamageEvent>()
                .IncReplicant<HideEvent>()
                .IncReplicant<ChargeGetFromPoolEvent>();
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