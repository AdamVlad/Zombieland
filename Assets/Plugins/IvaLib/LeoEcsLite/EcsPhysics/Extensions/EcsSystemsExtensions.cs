using Assets.Plugins.IvaLib.LeoEcsLite.EcsPhysics.Events;
using Leopotam.EcsLite;
using Leopotam.EcsLite.ExtendedSystems;

namespace Assets.Plugins.IvaLib.LeoEcsLite.EcsPhysics.Extensions
{
    public static class EcsSystemsExtensions
    {
        public static IEcsSystems DelHerePhysics(this IEcsSystems ecsSystems, string worldName = null)
        {
            ecsSystems.DelHere<OnTriggerEnterEvent>(worldName);
            ecsSystems.DelHere<OnCollisionEnterEvent>(worldName);

            return ecsSystems;
        }
    }
}