using Assets.Game.Scripts.Levels.Model.Components.Data;
using Assets.Game.Scripts.Levels.Model.Components.Data.Enemies;

using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Components.Behaviours
{
    [RequireComponent(typeof(BehavioursScope))]
    internal sealed class DeathBehaviour : MonoBehaviour, IBehaviour
    {
        [Inject]
        private void Construct(
            EcsWorld world)
        {
            _world = world;
        }

        private void Start()
        {
            _enemy = GetComponent<Enemy>();
            _healthPool = _world.GetPool<HealthComponent>();
        }

        public float Evaluate()
        {
            if (!_enemy.Unpack(_world, out var enemyEntity)) return 0;

            ref var healthComponent = ref _healthPool.Get(enemyEntity);
            return healthComponent.CurrentHealth <= 0 ? 2 : 0;
        }

        public void Behave()
        {
            if (!_enemy.Unpack(_world, out var enemyEntity)) return;

            _world.DelEntity(enemyEntity);
            Destroy(gameObject);
        }

        private EcsWorld _world;
        private EcsPool<HealthComponent> _healthPool;
        private Enemy _enemy;
    }
}