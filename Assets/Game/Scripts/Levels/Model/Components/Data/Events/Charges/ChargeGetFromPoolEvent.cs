using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;

namespace Assets.Game.Scripts.Levels.Model.Components.Data.Events.Charges
{
    internal struct ChargeGetFromPoolEvent : IEventReplicant
    {
        public int Entity;
    }
}