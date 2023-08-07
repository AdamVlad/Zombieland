using Leopotam.EcsLite;
using System.Runtime.CompilerServices;

namespace Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions
{
    public static class EcsSystemsExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEcsSystems Add(this IEcsSystems system, IEcsSystem toAdd, bool isPerformed)
        {
            return isPerformed ? system.Add(toAdd) : system;
        }
    }
}