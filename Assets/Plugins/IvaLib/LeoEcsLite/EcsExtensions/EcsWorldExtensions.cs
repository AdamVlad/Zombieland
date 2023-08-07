using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Runtime.CompilerServices;

namespace Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions
{
    public static class EcsWorldExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DelEntity(this EcsWorldInject world, int entity)
        {
            world.Value.DelEntity(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int NewEntity(this EcsWorldInject world)
        {
            return world.Value.NewEntity();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EcsPackedEntity PackEntity(this EcsWorldInject world, int entity)
        {
            return world.Value.PackEntity(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T Add<T>(this EcsWorld world, int entity) where T : struct
        {
            return ref world.GetPool<T>().Add(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T Add<T>(this IEcsSystems systems, int entity) where T : struct
        {
            return ref Add<T>(systems.GetWorld(), entity);
        }
    }
}