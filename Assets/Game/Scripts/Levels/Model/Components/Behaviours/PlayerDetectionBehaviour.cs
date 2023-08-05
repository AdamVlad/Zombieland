using Assets.Game.Scripts.Levels.Model.Components.Data.Delayed;
using Assets.Game.Scripts.Levels.Model.Components.Data.Enemies;
using Assets.Game.Scripts.Levels.Model.ScriptableObjects;

using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Components.Behaviours
{
    // Подумать над тем, чтобы выставить приоритеты Evaluate в отдельной сущности
    // Вынести базовый класс для Behaviour
    [RequireComponent(
        typeof(Enemy),
        typeof(NavMeshAgent),
        typeof(BehavioursScope))]
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
            _agent = GetComponent<NavMeshAgent>();

            _detectionRadius = _enemy.Settings.DetectionRadius;
            _layerMask = _enemy.Settings.DetectionMask;

            _agent.speed = _enemy.Settings.MoveSpeed / _gameSettings.EnemyMoveSpeedDivider;

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
            _agent.SetDestination(_hitColliders[0].transform.position);
        }

        private EcsWorld _world;

        private EcsPool<AttackDelayed> _attackDelayedPool;

        private Enemy _enemy;
        private NavMeshAgent _agent;

        private GameConfigurationSo _gameSettings;
        private readonly Collider[] _hitColliders = new Collider[5];
        private float _detectionRadius;
        private LayerMask _layerMask;
    }
}