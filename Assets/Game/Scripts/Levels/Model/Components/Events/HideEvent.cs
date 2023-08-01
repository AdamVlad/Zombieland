using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;

namespace Assets.Game.Scripts.Levels.Model.Components.Events
{
    internal struct HideEvent : IEventReplicant
    {
        public int Entity;
    }
}