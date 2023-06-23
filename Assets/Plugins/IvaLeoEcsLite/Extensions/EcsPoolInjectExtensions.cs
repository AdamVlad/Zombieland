using Leopotam.EcsLite.Di;

namespace Assets.Plugins.IvaLeoEcsLite.Extensions
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
    }
}