using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions
{
    public static class EcsWorldInjectExtensions
    {
        public static void DelEntity(this EcsWorldInject world, int entity)
        {
            world.Value.DelEntity(entity);
        }

        public static int NewEntity(this EcsWorldInject world)
        {
            return world.Value.NewEntity();
        }

        public static EcsPackedEntity PackEntity(this EcsWorldInject world, int entity)
        {
            return world.Value.PackEntity(entity);
        }
    }
}