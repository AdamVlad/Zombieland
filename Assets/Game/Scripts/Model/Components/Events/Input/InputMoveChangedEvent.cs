using Assets.Plugins.IvaLeoEcsLite.EcsEvents;
using UnityEngine;

namespace Assets.Game.Scripts.Model.Components.Events.Input
{
    internal struct InputMoveChangedEvent : IEventSingleton
    {
        public Vector2 Axis;
    }
}