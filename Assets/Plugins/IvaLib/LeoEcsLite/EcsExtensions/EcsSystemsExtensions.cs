using Leopotam.EcsLite;

namespace Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions
{
    public static class EcsSystemsExtensions
    {
        public static IEcsSystems Add(this IEcsSystems system, IEcsSystem toAdd, bool isPerformed)
        {
            return isPerformed ? system.Add(toAdd) : system;
        }
    }
}