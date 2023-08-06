using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;
using Leopotam.EcsLite;

namespace Assets.Game.Scripts.Levels.Model.Components.Data.Events
{
    internal struct GetDamageEvent : IEventReplicant
    {
        public EcsPackedEntity From;
        public EcsPackedEntity To;
        public float Damage;
    }
}