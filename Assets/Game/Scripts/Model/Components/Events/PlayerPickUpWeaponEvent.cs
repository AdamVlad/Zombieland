using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;

namespace Assets.Game.Scripts.Model.Components.Events
{
    internal struct PlayerPickUpWeaponEvent : IEventSingleton
    {
        public int PlayerEntity;
        public int WeaponEntity;
    }
}