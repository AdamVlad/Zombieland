using Assets.Plugins.IvaLeoEcsLite.EcsEvents;
using UnityEngine;

namespace Assets.Game.Scripts.Model.Components.Events.Input
{
    internal struct InputShootEvent : IEventSingleton
    {
        public Vector2 ScreenPosition;
    }
}
