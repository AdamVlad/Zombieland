using Assets.Game.Scripts.Levels.Model.Components.Data.Weapons;
using Assets.Game.Scripts.Levels.Model.Practices.Builders;
using Assets.Game.Scripts.Levels.Model.Practices.Builders.Context;
using Assets.Plugins.IvaLib.UnityLib.Factory;

using Leopotam.EcsLite;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Practices.Factories
{
    internal class WeaponFactory : IFactory<Weapon, Weapon>
    {
        public WeaponFactory(EcsWorld world, Transform parent = null)
        {
            _world = world;
            _parent = parent;
        }

        public Weapon Create(Weapon prefab, Vector3 position = default)
        {
            var builder = new WeaponBuilder(new EcsContext(_world));

            return builder
                .WithWeapon()
                .WithClip()
                .WithWeaponShooting()
                .WithDamage()
                .WithAttackDelay()
                .WithReloadingDelay()
                .WithPrefab(prefab)
                .WithParentInitialize(_parent)
                .WithPositionInitialize(position)
                .WithTransform()
                .WithCollider()
                .WithRigidbody()
                .WithEntityReference()
                .WithParent()
                .Build();
        }

        private readonly Transform _parent;
        private readonly EcsWorld _world;
    }
}