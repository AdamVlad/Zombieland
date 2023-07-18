using Assets.Game.Scripts.Levels.Model.Components.Enemies;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;
using Assets.Plugins.IvaLib.UnityLib.Factory;
using Leopotam.EcsLite;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Factories
{
    internal class EnemyFactory : IFactory<Enemy, Enemy>
    {
        public EnemyFactory(EcsWorld world, Transform parent = null)
        {
            _world = world;
            _parent = parent;
        }

        public Enemy Create(Enemy prefab, Vector3 position)
        {
            var enemyGo = Object.Instantiate(
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

            return enemyGo;
        }

        private readonly Transform _parent;
        private readonly EcsWorld _world;
    }
}