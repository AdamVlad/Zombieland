using Assets.Game.Scripts.Levels.Model.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Components.Enemies
{
    [RequireComponent(typeof(Enemy))]
    internal sealed class DetectionBehaviour : MonoBehaviour, IBehaviour
    {
        [Inject]
        private void Construct(
            GameConfigurationSo gameSettings)
        {
            _gameSettings = gameSettings;
        }

        private void Start()
        {
            var enemy = GetComponent<Enemy>();
            _detectionRadius = enemy.EnemySettings.DetectionRadius;
            _layerMask = enemy.EnemySettings.DetectionMask;
            _moveSpeed = enemy.EnemySettings.MoveSpeed / _gameSettings.MoveSpeedDivider;
        }

        public float Evaluate()
        {
            var hits = Physics.OverlapSphereNonAlloc(
                transform.position,
                _detectionRadius,
                _hitColliders,
                _layerMask);

            return hits != 0 ? 1 : 0;
        }

        public void Behave()
        {
            // Размер шага равен скорость * время кадра.
            float step = _moveSpeed;

            // Переместите нашу позицию на шаг ближе к цели.
            transform.position = Vector3.MoveTowards(transform.position, _hitColliders[0].transform.position, step);
            transform.LookAt(_hitColliders[0].transform);
        }

        private GameConfigurationSo _gameSettings;
        private Collider[] _hitColliders = new Collider[5];
        private float _detectionRadius;
        private LayerMask _layerMask;
        private float _moveSpeed;
    }
}