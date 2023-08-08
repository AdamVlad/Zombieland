using Assets.Game.Scripts.Levels.Model.Components.Data.Enemies;
using Assets.Game.Scripts.Levels.Model.Practices.Builders;
using Assets.Game.Scripts.Levels.Model.Practices.Builders.Context;

using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Practices.Factories
{
    internal class EnemyFactory : Plugins.IvaLib.UnityLib.Factory.IFactory<Enemy, Enemy>
    {
        [Inject] private readonly DiContainer _container;

        public EnemyFactory(EcsWorld world, Transform parent = null)
        {
            _world = world;
            _parent = parent;
        }

        public Enemy Create(Enemy prefab, Vector3 position = default)
        {
            var builder = new EnemyBuilder(new EcsContext(_world));

            return builder
                .WithEnemy()
                .WithHealth()
                .WithHpBar(false)
                .WithTag()
                .WithPrefab(prefab)
                .WithPositionInitialize(position)
                .WithParentInitialize(_parent)
                .WithTransform()
                .WithCollider()
                .WithAnimator()
                .WithNavMesh()
                .WithEntityReference()
                .WithBehaviours()
                .WithAttack()
                .Build(_container);
        }

        private readonly Transform _parent;
        private readonly EcsWorld _world;
    }
}