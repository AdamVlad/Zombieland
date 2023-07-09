using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;

namespace Assets.Game.Scripts.Levels.Model.Components.Events.Charges
{
    internal struct ChargeReturnToPoolEvent : IEventReplicant
    {
        public int Entity;
    }
}