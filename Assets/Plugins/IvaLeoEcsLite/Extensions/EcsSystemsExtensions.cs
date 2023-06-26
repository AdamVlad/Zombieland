using Leopotam.EcsLite;

namespace Assets.Plugins.IvaLeoEcsLite.Extensions
{
    public static class EcsSystemsExtensions
    {
        public static IEcsSystems Add(this IEcsSystems system, IEcsSystem toAdd, bool isPerformed)
        {
            return isPerformed ? system.Add(toAdd) : system;
        }
    }
}