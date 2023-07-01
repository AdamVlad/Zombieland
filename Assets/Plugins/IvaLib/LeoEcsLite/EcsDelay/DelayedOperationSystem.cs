using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Plugins.IvaLib.LeoEcsLite.EcsDelay
{
    public sealed class DelayedOperationSystem<T> : IEcsRunSystem where T : struct
    {
        private readonly EcsFilterInject<Inc<Delayed<T>>> _delayedFilter = default;
        private readonly EcsPoolInject<T> _delayedComponentPool = default;

        private readonly EcsWorldInject _world = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _delayedFilter.Value)
            {
                ref var delayed = ref _delayedFilter.Get1(entity);

                delayed.TimeLeft -= Time.deltaTime;

                if (delayed.TimeLeft <= 0 &&
                    delayed.Target.Unpack(systems.GetWorld(), out var targetEntity))
                {
                    _delayedComponentPool.Add(targetEntity);
                    _world.DelEntity(entity);
                }
            }
        }
    }
}