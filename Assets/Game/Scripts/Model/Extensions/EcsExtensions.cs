using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Game.Scripts.Model.Extensions
{
    public static class EcsExtensions
    {
        public static void SendEvent<T>(this EcsWorld world, in T messageEvent) 
            where T : struct
        {
            var pool = world.GetPool<T>();
            var entity = world.NewEntity();
            ref var value = ref pool.Add(entity);
            value = messageEvent;
        }

        public static void SendEvent<T>(this EcsPoolInject<T> pool, in T messageEvent) 
            where T : struct
        {
            ref var value = ref pool.NewEntity(out _);
            value = messageEvent;
        }

        public static void SendEvent<T>(this EcsPoolInject<T> pool, in int entity, in T messageEvent)
            where T : struct
        {
            if (pool.Value.Has(entity))
            {
                pool.Value.Del(entity);
            }

            ref var value = ref pool.Value.Add(entity);
            value = messageEvent;
        }

        public static ref T SendEvent<T>(this EcsPool<T> pool, out int entity) 
            where T : struct
        {
            var world = pool.GetWorld();
            entity = world.NewEntity();
            return ref pool.Add(entity);
        }
    }
}
