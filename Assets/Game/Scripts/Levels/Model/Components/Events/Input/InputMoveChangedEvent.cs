using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Components.Events.Input
{
    internal struct InputMoveChangedEvent : IEventSingleton
    {
        public Vector2 Axis;
    }
}