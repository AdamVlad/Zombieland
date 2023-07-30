using AB_Utility.FromSceneToEntityConverter;
using Assets.Game.Scripts.Levels.Model.Components;
using Assets.Game.Scripts.Levels.Model.Components.Player;
using Assets.Game.Scripts.Levels.Model.ScriptableObjects;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsPhysics.Checkers;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents.EntityReference;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Systems.Player
{
    internal sealed class PlayerInitSystem : IEcsInitSystem
    {
        private readonly EcsFilterInject<Inc<PlayerTagComponent>> _filter = default;

        [Inject] private PlayerConfigurationSo _playerSettings;
        [Inject] private GameConfigurationSo _gameSettings;

        private readonly EcsPoolInject<MonoLink<EntityReference>> _entityReferenceComponentPool = default;
        private readonly EcsPoolInject<MonoLink<Transform>> _transformComponentPool = default;
        private readonly EcsPoolInject<MonoLink<Rigidbody>> _rigidbodyComponentPool = default;
        private readonly EcsPoolInject<PlayerMoveComponent> _moveComponentPool = default;
        private readonly EcsPoolInject<RotationComponent> _rotationComponentPool = default;
        private readonly EcsPoolInject<InputComponent> _inputComponentPool = default;
        private readonly EcsPoolInject<ShootingComponent> _shootingComponentPool = default;
        private readonly EcsPoolInject<BackpackComponent> _backpackComponentPool = default;

        public void Init(IEcsSystems systems)
        {
            var playerGO = EcsConverter.InstantiateAndCreateEntity(
                _playerSettings.Prefab,
                Vector3.zero,
                Quaternion.identity,
                systems.GetWorld());

            var entityReference = playerGO.AddComponent<EntityReference>();
            playerGO.AddComponent<OnCollisionEnterChecker>();
            playerGO.AddComponent<OnTriggerEnterChecker>();

            foreach (var entity in _filter.Value)
            {
                _inputComponentPool.Add(entity);
                _shootingComponentPool.Add(entity);

                ref var entityReferenceComponent = ref _entityReferenceComponentPool.Add(entity);
                ref var transformComponent = ref _transformComponentPool.Add(entity);
                ref var rigidbodyComponent = ref _rigidbodyComponentPool.Add(entity);
                ref var moveComponent = ref _moveComponentPool.Add(entity);
                ref var rotationComponent = ref _rotationComponentPool.Add(entity);

                ref var backpackComponent = ref _backpackComponentPool.Get(entity);

                entityReference.Pack(entity);
                entityReferenceComponent.Value = entityReference;

                transformComponent.Value = playerGO.transform;
                rigidbodyComponent.Value = playerGO.GetComponent<Rigidbody>();

                moveComponent.Speed = _playerSettings.MoveSpeed / _gameSettings.PlayerMoveSpeedDivider;
                rotationComponent.Speed = _playerSettings.RotationSpeed / _gameSettings.RotationSpeedDivider;
                rotationComponent.SmoothTurningAngle = _playerSettings.SmoothTurningAngle;
                backpackComponent.WeaponEntity = -1;
            }
        }
    }
}