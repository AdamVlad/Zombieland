using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions
{
    public static class EcsPoolInjectExtensions
    {
        public static bool Has<T>(this EcsPoolInject<T> pools, int entity) where T : struct
        {
            return pools.Value.Has(entity);
        }

        public static ref T Get<T>(this EcsPoolInject<T> pools, int entity) where T : struct
        {
            return ref pools.Value.Get(entity);
        }

        public static ref T Add<T>(this EcsPoolInject<T> pools, int entity) where T : struct
        {
            return ref pools.Value.Add(entity);
        }

        public static void Del<T>(this EcsPoolInject<T> pools, int entity) where T : struct
        {
            pools.Value.Del(entity);
        }

        public static EcsWorld GetWorld<T>(this EcsPoolInject<T> pools) where T : struct
        {
            return pools.Value.GetWorld();
        }
    }
}