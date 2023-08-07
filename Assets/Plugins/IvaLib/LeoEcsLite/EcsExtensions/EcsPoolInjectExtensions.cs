using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Runtime.CompilerServices;

namespace Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions
{
    public static class EcsPoolInjectExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Has<T>(this EcsPoolInject<T> pools, int entity) where T : struct
        {
            return pools.Value.Has(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T Get<T>(this EcsPoolInject<T> pools, int entity) where T : struct
        {
            return ref pools.Value.Get(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T Add<T>(this EcsPoolInject<T> pools, int entity) where T : struct
        {
            return ref pools.Value.Add(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Del<T>(this EcsPoolInject<T> pools, int entity) where T : struct
        {
            pools.Value.Del(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EcsWorld GetWorld<T>(this EcsPoolInject<T> pools) where T : struct
        {
            return pools.Value.GetWorld();
        }
    }
}