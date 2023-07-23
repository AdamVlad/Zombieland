using Assets.Game.Scripts.Levels.Model.Components.Enemies;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Factories
{
    internal class EnemyFactory : Plugins.IvaLib.UnityLib.Factory.IFactory<Enemy, Enemy>
    {
        [Inject] private DiContainer _container;

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

            return enemyGo.GetComponent<Enemy>();
        }

        private readonly Transform _parent;
        private readonly EcsWorld _world;
    }
}