using Assets.Game.Scripts.Levels.Model.Components;
using Assets.Game.Scripts.Levels.Model.Components.Enemies;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents.EntityReference;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Factories
{
    internal class EnemyFactory : Plugins.IvaLib.UnityLib.Factory.IFactory<Enemy, Enemy>
    {
        [Inject] private DiContainer _container;

        // Сделать добавление компонентов через .
        public EnemyFactory(EcsWorld world, Transform parent = null)
        {
            _world = world;
            _parent = parent;
        }

        public Enemy Create(Enemy prefab, Vector3 position)
        {
            var enemyGo = _container.InstantiatePrefab(
                prefab,
                position,
                Quaternion.identity,
                _parent);

            var enemyEntity = _world.NewEntity();

            // Enemy
            var enemy = enemyGo.GetComponent<Enemy>();
            enemy.Pack(_world, enemyEntity);
            var enemyPool = _world.GetPool<MonoLink<Enemy>>();
            ref var enemyComponent = ref enemyPool.Add(enemyEntity);
            enemyComponent.Value = enemy;

            // EnemyTag
            var enemyTagPool = _world.GetPool<EnemyTagComponent>();
            enemyTagPool.Add(enemyEntity);

            // EntityReference
            var entityReference = enemyGo.AddComponent<EntityReference>();
            var entityReferencePool = _world.GetPool<MonoLink<EntityReference>>();
            ref var entityReferenceComponent = ref entityReferencePool.Add(enemyEntity);
            entityReference.Pack(enemyEntity);
            entityReferenceComponent.Value = entityReference;

            // Transform
            var transformPool = _world.GetPool<MonoLink<Transform>>();
            ref var transform = ref transformPool.Add(enemyEntity);
            transform.Value = enemyGo.transform;

            // Collider
            var colliderPool = _world.GetPool<MonoLink<Collider>>();
            ref var collider = ref colliderPool.Add(enemyEntity);
            collider.Value = enemyGo.GetComponent<Collider>();

            // Behaviours
            var behaviourPool = _world.GetPool<BehaviourComponent>();
            behaviourPool.Add(enemyEntity);
            var behavioursScopePool = _world.GetPool<MonoLink<BehavioursScope>>();
            ref var behaviours = ref behavioursScopePool.Add(enemyEntity);
            behaviours.Value = enemyGo.GetComponent<BehavioursScope>();

            // Animation
            var animationPool = _world.GetPool<MonoLink<Animator>>();
            ref var animatorComponent = ref animationPool.Add(enemyEntity);
            animatorComponent.Value = enemyGo.GetComponentInChildren<Animator>();

            // NavMesh
            var navMeshPool = _world.GetPool<MonoLink<NavMeshAgent>>();
            ref var navMeshAgentComponent = ref navMeshPool.Add(enemyEntity);
            navMeshAgentComponent.Value = enemyGo.GetComponent<NavMeshAgent>();

            // Attack
            var attackPool = _world.GetPool<ShootingComponent>();
            attackPool.Add(enemyEntity);

            // Health
            var healthPool = _world.GetPool<HealthComponent>();
            ref var healthComponent = ref healthPool.Add(enemyEntity);
            healthComponent.MaxHealth = enemy.Settings.MaxHealth;
            healthComponent.CurrentHealth = enemy.Settings.MaxHealth;

            // HpBar
            var hpBarPool = _world.GetPool<HpBarComponent>();
            ref var hpBarComponent = ref hpBarPool.Add(enemyEntity);
            hpBarComponent.HpBarCanvas = enemy.HpBarCanvas;
            hpBarComponent.HpBarCanvas.enabled = false;
            hpBarComponent.Fill = enemy.HpImageFill;

            return enemy;
        }

        private readonly Transform _parent;
        private readonly EcsWorld _world;
    }
}