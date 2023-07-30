using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/GameSettings")]
    internal class GameConfigurationSo : ScriptableObject
    {
        [SerializeField] private int _playerMoveSpeedDivider;
        public int PlayerMoveSpeedDivider => _playerMoveSpeedDivider;

        [SerializeField] private int _enemyMoveSpeedDivider;
        public int EnemyMoveSpeedDivider => _enemyMoveSpeedDivider;

        [SerializeField] private int _rotationSpeedDivider;
        public int RotationSpeedDivider => _rotationSpeedDivider;

        [SerializeField, Range(1, 10)] private float _shootingPowerDivider;
        public float ShootingPowerDivider => _shootingPowerDivider;
    }
}