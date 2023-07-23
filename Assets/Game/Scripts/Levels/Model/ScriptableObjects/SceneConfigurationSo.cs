using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.ScriptableObjects
{
    [CreateAssetMenu(fileName = "SceneSettings", menuName = "EnemySettings/SceneSettings")]
    internal class SceneConfigurationSo : ScriptableObject
    {
        [Header("Shooting raycastable")]
        [SerializeField] private LayerMask _raycastableMask;
        public LayerMask RaycastableMask => _raycastableMask;

        [Space, Header("Spawners")] 
        [SerializeField] private float _weaponSpawnDelay;
        public float WeaponSpawnDelay => _weaponSpawnDelay;

        [SerializeField, Range(0, 360)] private float _weaponSpawnerRotationAngle = 360;
        public float WeaponSpawnerRotationAngle => _weaponSpawnerRotationAngle;

        [SerializeField, Range(0, 10)] private float _weaponSpawnerRotationDuration = 2.5f;
        public float WeaponSpawnerRotationDuration => _weaponSpawnerRotationDuration;

        [SerializeField, Range(0, 2)] private float _weaponSpawnerClimbingPosition = 0.5f;
        public float WeaponSpawnerClimbingPosition => _weaponSpawnerClimbingPosition;

        [SerializeField, Range(0, 10)] private float _weaponSpawnerClimbingDuration = 1.6f;
        public float WeaponSpawnerClimbingDuration => _weaponSpawnerClimbingDuration;

        [Space, Header("Destructions")]
        [SerializeField, Range(0,5)] private float _weaponDestructionTime = 0.8f;
        public float WeaponDestructionTime => _weaponDestructionTime;

    }
}