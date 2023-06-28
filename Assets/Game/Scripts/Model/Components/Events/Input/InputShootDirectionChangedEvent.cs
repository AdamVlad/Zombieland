using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;
using UnityEngine;

namespace Assets.Game.Scripts.Model.Components.Events.Input
{
    internal struct InputShootDirectionChangedEvent : IEventSingleton
    {
        public Vector2 ScreenInputPosition;
    }
}