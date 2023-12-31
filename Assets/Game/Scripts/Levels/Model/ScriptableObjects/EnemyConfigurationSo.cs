﻿using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Settings", menuName = "Settings/Settings")]
    internal sealed class EnemyConfigurationSo : ScriptableObject
    {
        [Space, Header("Health")]

        [SerializeField, Range(1, 1000)] private float _maxHealth;
        public float MaxHealth => _maxHealth;


        [SerializeField, Range(1, 10)] private float _healthBarHideDelay = 2.5f;
        public float HealthBarHideDelay => _healthBarHideDelay;

        [Space, Header("Moving")]

        [SerializeField, Range(1, 1000)] private float _moveSpeed;
        public float MoveSpeed => _moveSpeed;

        [Space, Header("Attack")]

        [SerializeField, Range(0.1f, 50)] private int _damage;
        public float Damage => _damage;

        [SerializeField, Range(0.1f, 50)] private float _attackRadius;
        public float AttackRadius => _attackRadius;

        [SerializeField, Range(0, 5)] private float _attackDelay;
        public float AttackDelay => _attackDelay;

        [Header("Detection")]

        [SerializeField, Range(1, 100)] private float _detectionRadius;
        public float DetectionRadius => _detectionRadius;

        [SerializeField] private LayerMask _detectionMask;
        public LayerMask DetectionMask => _detectionMask;

        [Space, Header("Animations")]

        [SerializeField] private string _moveXParameter;
        public string MoveXParameter => _moveXParameter;

        [SerializeField] private string _moveYParameter;
        public string MoveYParameter => _moveYParameter;

        [SerializeField] private string _attackTriggerParameter;
        public string AttackTriggerParameter => _attackTriggerParameter;
    }
}