using Assets.Game.Scripts.Levels.Model.Components.Delayed;
using Assets.Game.Scripts.Levels.Model.ScriptableObjects;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Components.Enemies.Behaviours
{
    // Подумать над тем, чтобы выставить приоритеты Evaluate в отдельной сущности
    [RequireComponent(typeof(Enemy))]
    internal sealed class PlayerDetectionBehaviour : MonoBehaviour, IBehaviour
    {
        [Inject]
        private void Construct(
            EcsWorld world,
            GameConfigurationSo gameSettings)
        {
            _world = world;
            _gameSettings = gameSettings;
        }

        private void Start()
        {
            _enemy = GetComponent<Enemy>();

            _detectionRadius = _enemy.Settings.DetectionRadius;
            _layerMask = _enemy.Settings.DetectionMask;
            _moveSpeed = _enemy.Settings.MoveSpeed / _gameSettings.MoveSpeedDivider;

            _attackDelayedPool = _world.GetPool<AttackDelayed>();
        }

        public float Evaluate()
        {
            if (!_enemy.Unpack(_world, out var enemyEntity)) return 0;
            if (_attackDelayedPool.Has(enemyEntity)) return 0;

            var hits = Physics.OverlapSphereNonAlloc(
                transform.position,
                _detectionRadius,
                _hitColliders,
                _layerMask);

            return hits != 0 ? 0.8f : 0;
        }

        public void Behave()
        {
            var direction = (_hitColliders[0].transform.position - transform.position).normalized;

            transform.position += direction * _moveSpeed;

            transform.LookAt(_hitColliders[0].transform);
        }

        private EcsWorld _world;

        private EcsPool<AttackDelayed> _attackDelayedPool;

        private Enemy _enemy;
        private GameConfigurationSo _gameSettings;
        private Collider[] _hitColliders = new Collider[5];
        private float _detectionRadius;
        private LayerMask _layerMask;
        private float _moveSpeed;
    }
}