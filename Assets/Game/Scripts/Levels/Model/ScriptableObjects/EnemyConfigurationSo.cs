using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.ScriptableObjects
{
    [CreateAssetMenu(fileName = "EnemySettings", menuName = "EnemySettings/EnemySettings")]
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
    }
}