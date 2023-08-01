using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;

namespace Assets.Game.Scripts.Levels.Model.Components.Events.Charges
{
    internal struct ChargeGetFromPoolEvent : IEventReplicant
    {
        public int Entity;
    }
}