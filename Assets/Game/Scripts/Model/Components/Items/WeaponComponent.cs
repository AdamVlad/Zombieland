using System;

namespace Assets.Game.Scripts.Model.Components.Items
{
    [Serializable]
    internal struct WeaponComponent : IWeapon
    {
        public float TotalCharge { get; set; }
        public float CurrentCharge { get; set; }
        public float Damage { get; set; }
    }
}