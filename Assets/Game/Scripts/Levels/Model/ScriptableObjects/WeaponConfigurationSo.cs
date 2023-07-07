﻿using Assets.Game.Scripts.Levels.Model.Components.Weapons.Charges;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.ScriptableObjects
{
    [CreateAssetMenu(fileName = "WeaponSettings", menuName = "Settings/WeaponSettings")]
    internal sealed class WeaponConfigurationSo : ScriptableObject
    {
        [SerializeField, Range(1,1000)] private int _damage;
        public int Damage => _damage;

        [Space, Header("Charge")]

        [SerializeField] private ChargeType _chargeType;
        public ChargeType ChargeType => _chargeType;

        [SerializeField, Range(1, 1000)] private int _totalCharge;
        public int TotalCharge => _totalCharge;

        [SerializeField, Range(1, 1000)] private int _capacityCharge;
        public int CapacityCharge => _capacityCharge;

        [Space, Header("Shooting")]

        [SerializeField, Range(0, 5)] private float _shootingDelay;
        public float ShootingDelay => _shootingDelay;

        [SerializeField, Range(0, 50)] private float _shootingSpeed;
        public float ShootingSpeed => _shootingSpeed;

        [SerializeField, Range(0,5)] private float _reloadingTime;
        public float ReloadingTime => _reloadingTime;
    }
}