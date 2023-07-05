using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Components.Events.Input
{
    internal struct InputOnScreenStartedEvent : IEventSingleton
    {
        public Vector2 ScreenPosition;
    }
}