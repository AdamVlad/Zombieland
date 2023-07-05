using System;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Components.Weapons
{
    [Serializable]
    internal struct WeaponComponent : IWeapon
    {
        public GameObject Bullet;
        public float TotalCharge { get; set; }
        public float CurrentCharge { get; set; }
        public float Damage { get; set; }
    }
}