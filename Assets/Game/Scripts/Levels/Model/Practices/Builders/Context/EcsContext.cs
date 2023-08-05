using Assets.Game.Scripts.Levels.Model.Components.Behaviours;
using Assets.Game.Scripts.Levels.Model.Components.Data;
using Assets.Game.Scripts.Levels.Model.Components.Data.Enemies;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents.EntityReference;

using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Game.Scripts.Levels.Model.Practices.Builders.Context
{
    internal class EcsContext
    {
        public EcsContext(EcsWorld world)
        {
            _world = world;
            _entity = _world.NewEntity();
        }

        public EcsContext SetTransform(Transform transform)
        {
            var transformPool = _world.GetPool<MonoLink<Transform>>();
            ref var transformComponent = ref transformPool.Add(_entity);
            transformComponent.Value = transform;

            return this;
        }

        public EcsContext SetCollider(Collider collider)
        {
            var colliderPool = _world.GetPool<MonoLink<Collider>>();
            ref var colliderComponent = ref colliderPool.Add(_entity);
            colliderComponent.Value = collider;

            return this;
        }

        public EcsContext SetAnimator(Animator animator)
        {
            var animationPool = _world.GetPool<MonoLink<Animator>>();
            ref var animatorComponent = ref animationPool.Add(_entity);
            animatorComponent.Value = animator;

            return this;
        }

        public EcsContext SetNavMesh(NavMeshAgent navMesh)
        {
            var navMeshPool = _world.GetPool<MonoLink<NavMeshAgent>>();
            ref var navMeshAgentComponent = ref navMeshPool.Add(_entity);
            navMeshAgentComponent.Value = navMesh;

            return this;
        }

        public EcsContext SetEntityReference(EntityReference entityReference)
        {
            var entityReferencePool = _world.GetPool<MonoLink<EntityReference>>();
            ref var entityReferenceComponent = ref entityReferencePool.Add(_entity);
            entityReference.Pack(_entity);
            entityReferenceComponent.Value = entityReference;

            return this;
        }

        public EcsContext SetBehaviours(BehavioursScope behaviourScope)
        {
            var behaviourPool = _world.GetPool<BehaviourComponent>();
            behaviourPool.Add(_entity);
            var behavioursScopePool = _world.GetPool<MonoLink<BehavioursScope>>();
            ref var behaviours = ref behavioursScopePool.Add(_entity);
            behaviours.Value = behaviourScope;

            return this;
        }

        public EcsContext SetAttack()
        {
            var attackPool = _world.GetPool<ShootingComponent>();
            attackPool.Add(_entity);

            return this;
        }

        public EcsContext SetHealth(float value)
        {
            var healthPool = _world.GetPool<HealthComponent>();
            ref var healthComponent = ref healthPool.Add(_entity);
            healthComponent.MaxHealth = value;
            healthComponent.CurrentHealth = value;

            return this;
        }

        public EcsContext SetEnemy(Enemy enemy)
        {
            enemy.Pack(_world, _entity);
            var enemyPool = _world.GetPool<MonoLink<Enemy>>();
            ref var enemyComponent = ref enemyPool.Add(_entity);
            enemyComponent.Value = enemy;

            return this;
        }

        public EcsContext SetEnemyTag()
        {
            var enemyTagPool = _world.GetPool<EnemyTagComponent>();
            enemyTagPool.Add(_entity);

            return this;
        }

        public EcsContext SetEnemyHpBar(Enemy enemy)
        {
            var hpBarPool = _world.GetPool<HpBarComponent>();
            ref var hpBarComponent = ref hpBarPool.Add(_entity);
            hpBarComponent.HpBarCanvas = enemy.HpBarCanvas;
            hpBarComponent.HpBarCanvas.enabled = false;
            hpBarComponent.Fill = enemy.HpImageFill;

            return this;
        }

        protected readonly EcsWorld _world;
        protected readonly int _entity;
    }
}