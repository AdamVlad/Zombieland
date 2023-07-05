using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/GameSettings")]
    internal class GameConfigurationSo : ScriptableObject
    {
        [SerializeField] private int _moveSpeedDivider;
        public int MoveSpeedDivider => _moveSpeedDivider;

        [SerializeField] private int _rotationSpeedDivider;
        public int RotationSpeedDivider => _rotationSpeedDivider;
    }
}