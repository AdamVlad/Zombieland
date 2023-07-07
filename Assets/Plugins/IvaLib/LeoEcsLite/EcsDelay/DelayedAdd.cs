using Leopotam.EcsLite;

namespace Assets.Plugins.IvaLib.LeoEcsLite.EcsDelay
{
    public struct DelayedAdd<T> where T : struct
    {
        public EcsPackedEntity Target;
        public float TimeLeft;
    }
}
