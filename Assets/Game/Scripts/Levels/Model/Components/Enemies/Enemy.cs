using Assets.Game.Scripts.Levels.Model.ScriptableObjects;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Components.Enemies
{
    internal class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyConfigurationSo _enemySettings;
        public EnemyConfigurationSo EnemySettings => _enemySettings;

        [SerializeField] private GameConfigurationSo _gameSettings;
        public GameConfigurationSo GameSettings => _gameSettings;
    }
}