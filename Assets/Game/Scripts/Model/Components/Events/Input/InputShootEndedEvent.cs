using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;
using UnityEngine;

namespace Assets.Game.Scripts.Model.Components.Events.Input
{
    internal struct InputShootEndedEvent : IEventSingleton
    {
        public Vector2 ScreenPosition;
    }
}