﻿using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;

namespace Assets.Game.Scripts.Levels.Model.Components.Events
{
    internal struct PlayerReloadingEvent : IEventSingleton
    {
        public int PlayerEntity;
        public int WeaponEntity;
    }
}