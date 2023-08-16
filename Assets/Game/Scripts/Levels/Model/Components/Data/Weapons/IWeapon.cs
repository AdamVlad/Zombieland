using Assets.Game.Scripts.Levels.Model.Components.Data.Charges;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Components.Data.Weapons
{
    internal interface IWeapon
    {
        Sprite Icon { get; }
        public int Damage { get; }
        public float Cooldown { get; }
    }

    internal interface IRangedWeapon : IWeapon
    {
        Charge Charge { get; }
        public int ClipCapacity { get; }
        public float ReloadingTime { get; }
        public float ShootingDistance { get; }
        public float ShootingPower { get; }
    }

    internal interface IMeleeWeapon : IWeapon
    {

    }
}