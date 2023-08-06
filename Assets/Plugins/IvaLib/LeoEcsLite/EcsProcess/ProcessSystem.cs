using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Plugins.IvaLib.LeoEcsLite.EcsProcess
{
    public sealed class ProcessSystem<TProcess> : IEcsRunSystem where TProcess : struct, IProcessData
    {
        private readonly EcsFilterInject<Inc<TProcess, Process>> _filter = default;
        private readonly EcsFilterInject<Inc<Started<TProcess>>> _started = default;
        private readonly EcsFilterInject<Inc<Completed<TProcess>>> _completed = default;

        private readonly EcsPoolInject<Started<TProcess>> _startedPool = default;
        private readonly EcsPoolInject<Executing<TProcess>> _executingPool = default;
        private readonly EcsPoolInject<Completed<TProcess>> _completedPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _started.Value)
            {
                _startedPool.Value.Del(entity);
            }

            foreach (var entity in _completed.Value)
            {
                _completedPool.Value.Del(entity);
            }

            foreach (var entity in _filter.Value)
            {
                ref var process = ref _filter.Get2(entity);
                var world = systems.GetWorld();

                if (process.Phase == StatePhase.Complete)
                {
                    world.DelEntity(entity);
                    continue;
                }

                if (!process.Target.Unpack(world, out var targetEntity)) continue;

                if (process.Phase == StatePhase.OnStart)
                {
                    process.Phase = StatePhase.Process;
                    _executingPool.Value.Add(targetEntity) = new Executing<TProcess>(entity);
                }

                if (process.Paused)
                    continue;

                process.Duration -= UnityEngine.Time.deltaTime;
                if (process.Duration <= 0)
                {
                    process.Phase = StatePhase.Complete;
                    if (_executingPool.Value.Has(targetEntity))
                    {
                        _executingPool.Value.Del(targetEntity);
                    }

                    _completedPool.Value.Add(targetEntity) = new Completed<TProcess>(entity);
                }
            }
        }
    }
}