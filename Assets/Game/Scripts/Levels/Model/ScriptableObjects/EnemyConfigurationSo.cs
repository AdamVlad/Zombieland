using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Settings", menuName = "Settings/Settings")]
    internal sealed class EnemyConfigurationSo : ScriptableObject
    {
        [Header("Detection")]

        [SerializeField, Range(1, 100)] private float _detectionRadius;
        public float DetectionRadius => _detectionRadius;

        [SerializeField] private LayerMask _detectionMask;
        public LayerMask DetectionMask => _detectionMask;

        [Space, Header("Moving")] 
        
        [SerializeField, Range(1, 1000)] private float _moveSpeed;
        public float MoveSpeed => _moveSpeed;

        [Space, Header("Attack")]

        [SerializeField, Range(0.1f, 50)] private float _attackRadius;
        public float AttackRadius => _attackRadius;

        [SerializeField, Range(0, 5)] private float _attackDelay;
        public float AttackDelay => _attackDelay;
    }
}