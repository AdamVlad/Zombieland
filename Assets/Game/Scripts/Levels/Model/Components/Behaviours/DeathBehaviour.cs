using Assets.Game.Scripts.Levels.Model.Components.Behaviours.Interfaces;
using Assets.Game.Scripts.Levels.Model.Components.Data;
using Assets.Game.Scripts.Levels.Model.Components.Data.Delayed;
using Assets.Game.Scripts.Levels.Model.Components.Data.Enemies;

using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Components.Behaviours
{
    [RequireComponent(
        typeof(Enemy),
        typeof(BehavioursScope))]
    internal sealed class DeathBehaviour : MonoBehaviour, IBehaviour
    {
        [Inject] private readonly EcsWorld _world;

        private void Start()
        {
            _enemy = GetComponent<Enemy>();
            _healthPool = _world.GetPool<HealthComponent>();
            _destructionPool = _world.GetPool<DestructionDelayed>();
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

            _destructionPool.Add(enemyEntity);
        }

        private EcsPool<HealthComponent> _healthPool;
        private EcsPool<DestructionDelayed> _destructionPool;
        private Enemy _enemy;
    }
}