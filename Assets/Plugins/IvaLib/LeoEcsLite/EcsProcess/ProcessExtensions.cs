using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Plugins.IvaLib.LeoEcsLite.EcsProcess
{
    public static class ProcessExtensions
    {
        public static ref TProcess GetProcessData<TProcess>(in this Started<TProcess> eventData, EcsPool<TProcess> pool)
            where TProcess : struct
        {
            return ref pool.Get(eventData.ProcessEntity);
        }

        public static ref TProcess GetProcessData<TProcess>(in this Executing<TProcess> eventData,
            EcsPool<TProcess> pool) where TProcess : struct
        {
            return ref pool.Get(eventData.ProcessEntity);
        }

        public static ref TProcess GetProcessData<TProcess>(in this Completed<TProcess> eventData,
            EcsPool<TProcess> pool) where TProcess : struct
        {
            return ref pool.Get(eventData.ProcessEntity);
        }

        public static ref TProcess StartNewProcess<TProcess>(this EcsPoolInject<TProcess> pool, int targetEntity, float duration = 0)
            where TProcess : struct, IProcessData
        {
            return ref StartNewProcess(pool.Value, targetEntity, duration);
        }

        public static ref TProcess StartNewProcess<TProcess>(this EcsPool<TProcess> pool, int targetEntity, float duration = 0)
            where TProcess : struct, IProcessData
        {
            var world = pool.GetWorld();
            var processEntity = world.NewEntity();

            world.GetPool<Process>().Add(processEntity) = new Process
            {
                Phase = StatePhase.OnStart,
                Target = world.PackEntity(targetEntity),
                Duration = duration
            };

            world.GetPool<Started<TProcess>>().Add(targetEntity) = new Started<TProcess>(processEntity);

            return ref pool.Add(processEntity);
        }

        public static void SetDurationToProcess(this EcsPoolInject<Process> pool, int processEntity, float duration)
        {
            pool.Get(processEntity).Duration = duration;
        }

        public static void SetDurationToProcess(this EcsPool<Process> pool, int processEntity, float duration)
        {
            pool.Get(processEntity).Duration = duration;
        }

        public static void KillProcess<TProcess>(this EcsPoolInject<TProcess> pool, int targetEntity)
            where TProcess : struct, IProcessData
        {
            pool.Value.KillProcess(targetEntity);
        }

        public static void KillProcess<TProcess>(this EcsPool<TProcess> pool, int targetEntity)
            where TProcess : struct, IProcessData
        {
            var world = pool.GetWorld();

            var activeProcessPool = world.GetPool<Executing<TProcess>>();
            var allProcessPool = world.GetPool<Process>();

            var processEntity = activeProcessPool.Get(targetEntity).ProcessEntity;

            activeProcessPool.Del(targetEntity);
            allProcessPool.Del(processEntity);
            pool.Del(processEntity);
        }
    }
}