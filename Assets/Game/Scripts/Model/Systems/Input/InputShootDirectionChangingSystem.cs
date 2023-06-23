using Assets.Game.Scripts.Model.AppData;
using Assets.Game.Scripts.Model.Components;
using Assets.Plugins.IvaLeoEcsLite.UnityEcsComponents;
using Assets.Plugins.IvaLib;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Game.Scripts.Model.Systems.Input
{
    internal sealed class InputShootDirectionChangingSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                InputComponent,
                MonoLink<Transform>,
                ShootingComponent>> _shootingFilter = default;

        private readonly EcsFilterInject<Inc<InputScreenPositionComponent>> _screenFilter = default;

        private readonly EcsSharedInject<SharedData> _sharedData = default;

        public void Run(IEcsSystems systems)
        {
            var shootingPools = _shootingFilter.Pools;
            var screenPools = _screenFilter.Pools;

            foreach (var entity in _shootingFilter.Value)
            {
                ref var shootingComponent = ref shootingPools.Inc3.Get(entity);

                if (!shootingComponent.IsShooting) continue;

                ref var transform = ref shootingPools.Inc2.Get(entity).Value;

                foreach (var screenEntity in _screenFilter.Value)
                {
                    ref var screenInputPosition = ref screenPools.Inc1.Get(screenEntity).Position;

                    if (!ScreenPointToWorldConverter.GetWorldPointFrom(
                            ref screenInputPosition,
                            _sharedData.Value.MainCamera,
                            _sharedData.Value.RaycastableMask,
                            out var shootDirectionPoint
                        )) return;

                    shootingComponent.Direction = (shootDirectionPoint - transform.position).normalized;
                }
            }
        }
    }
}
