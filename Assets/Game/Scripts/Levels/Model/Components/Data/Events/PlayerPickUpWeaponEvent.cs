using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;

namespace Assets.Game.Scripts.Levels.Model.Components.Data.Events
{
    internal struct PlayerPickUpWeaponEvent : IEventSingleton
    {
        public int PlayerEntity;
        public int WeaponEntity;
    }
}