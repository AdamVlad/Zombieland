using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;

namespace Assets.Game.Scripts.Levels.Model.Components.Data.Events
{
    internal struct GetDamageEvent : IEventReplicant
    {
        public int From;
        public int To;
        public float Damage;
    }
}