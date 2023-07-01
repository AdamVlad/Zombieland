using Leopotam.EcsLite;

namespace Assets.Plugins.IvaLib.LeoEcsLite.EcsDelay
{
    public struct Delayed<T> where T : struct
    {
        public EcsPackedEntity Target;
        public float TimeLeft;
    }
}
