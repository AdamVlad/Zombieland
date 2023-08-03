using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Components.Data.Events.Input
{
    internal struct InputOnScreenEndedEvent : IEventSingleton
    {
        public Vector2 ScreenPosition;
    }
}