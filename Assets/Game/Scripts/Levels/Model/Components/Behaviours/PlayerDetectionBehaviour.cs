using Assets.Game.Scripts.Levels.Model.Components.Behaviours.Base;
using Assets.Game.Scripts.Levels.Model.Components.Data.Delayed;
using Assets.Game.Scripts.Levels.Model.Components.Data.Enemies;
using Assets.Game.Scripts.Levels.Model.ScriptableObjects;

using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Components.Behaviours
{

    [RequireComponent(
        typeof(Enemy),
        typeof(NavMeshAgent))]
    internal sealed class PlayerDetectionBehaviour : CollisionBehaviourBase
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

            Initialize(
                5,
                _enemy.Settings.DetectionRadius,
                _enemy.Settings.DetectionMask);

            _agent.speed = _enemy.Settings.MoveSpeed / _gameSettings.EnemyMoveSpeedDivider;
            _attackDelayedPool = _world.GetPool<AttackDelayed>();
        }

        public override float Evaluate()
        {
            if (!_enemy.Unpack(_world, out var enemyEntity)) return 0;
            if (_attackDelayedPool.Has(enemyEntity)) return 0;

            var hits = Physics.OverlapSphereNonAlloc(
                transform.position,
                CollisionRadius,
                HittedColliders,
                CollisionLayers);

            return hits != 0 ? 0.8f : 0;
        }

        public override void Behave()
        {
            _agent.SetDestination(HittedColliders[0].transform.position);
        }

        private EcsWorld _world;
        private EcsPool<AttackDelayed> _attackDelayedPool;

        private Enemy _enemy;
        private NavMeshAgent _agent;

        private GameConfigurationSo _gameSettings;
    }
}