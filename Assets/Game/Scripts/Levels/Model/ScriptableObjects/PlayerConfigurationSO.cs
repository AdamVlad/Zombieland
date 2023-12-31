﻿using Assets.Game.Scripts.Levels.Model.Components.Data.Weapons;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "Settings/PlayerSettings")]
    internal class PlayerConfigurationSo : ScriptableObject
    {
        [Space, SerializeField] private Sprite _icon;
        public Sprite Icon => _icon;

        [Space, Header("Health")]

        [SerializeField, Range(1, 1000)] private float _maxHealth;
        public float MaxHealth => _maxHealth;

        [Space, Header("Physics")]

        [SerializeField] private float _moveSpeed;
        public float MoveSpeed => _moveSpeed;

        [SerializeField] private float _rotationSpeed;
        public float RotationSpeed => _rotationSpeed;

        [SerializeField] private float _smoothTurningAngle;
        public float SmoothTurningAngle => _smoothTurningAngle;

        [Space, Header("Animation")]

        [SerializeField] private string _moveXParameter;
        public string MoveXParameter => _moveXParameter;

        [SerializeField] private string _moveYParameter;
        public string MoveYParameter => _moveYParameter;

        [SerializeField] private string _isShootingParameter;
        public string IsShootingParameter => _isShootingParameter;

        [SerializeField] private string _shootParameter;
        public string ShootParameter => _shootParameter;

        [Space, Header("RangedWeapon")]
        [SerializeField] private RangedWeapon _weaponPrefab;
        public RangedWeapon WeaponPrefab => _weaponPrefab;
    }
}