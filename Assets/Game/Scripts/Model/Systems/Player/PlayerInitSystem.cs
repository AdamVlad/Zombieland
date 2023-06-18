using AB_Utility.FromSceneToEntityConverter;
using Assets.Game.Scripts.Model.Components;
using Assets.Game.Scripts.Model.ScriptableObjects;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Game.Scripts.Model.Systems.Player
{
    internal sealed class PlayerInitSystem : IEcsInitSystem
    {
        private readonly EcsCustomInject<BobConfigurationSO> _bobSettings = default;

        private readonly EcsFilterInject<Inc<PlayerTagComponent>> _filter = default;

        private readonly EcsPoolInject<MoveComponent> _moveComponentPool = default;
        private readonly EcsPoolInject<RotationComponent> _rotationComponentPool = default;
        private readonly EcsPoolInject<BackpackComponent> _backpackComponentPool = default;

        public void Init(IEcsSystems systems)
        {
            EcsConverter.InstantiateAndCreateEntity(_bobSettings.Value.Prefab, Vector3.zero,
                Quaternion.identity, systems.GetWorld());

            foreach (var entity in _filter.Value)
            {
                ref var moveComponent = ref _moveComponentPool.Value.Add(entity);
                ref var rotationComponent = ref _rotationComponentPool.Value.Add(entity);

                ref var backpackComponent = ref _backpackComponentPool.Value.Get(entity);

                moveComponent.Speed = _bobSettings.Value.MoveSpeed / 1000; // Сделать настройки игры и задать параметр срезания скорости
                rotationComponent.Speed = _bobSettings.Value.RotationSpeed / 5;
                rotationComponent.SmoothTurningAngle = _bobSettings.Value.SmoothTurningAngle;
                backpackComponent.WeaponEntity = -1;
            }
        }
    }
}