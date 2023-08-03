using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;

namespace Assets.Game.Scripts.Levels.Model.Components.Data.Events.Charges
{
    internal struct ChargeReturnToPoolEvent : IEventReplicant
    {
        public int Entity;
    }
}