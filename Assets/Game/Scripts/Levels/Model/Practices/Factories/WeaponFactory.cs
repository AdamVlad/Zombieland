using Assets.Game.Scripts.Levels.Model.Components.Data.Weapons;
using Assets.Game.Scripts.Levels.Model.Practices.Builders;
using Assets.Game.Scripts.Levels.Model.Practices.Builders.Context;
using Assets.Game.Scripts.Levels.Model.Practices.Pools;
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

            var chargeFactory = new ChargesFactory(_world);
            var chargesPool = new ChargesPool(prefab.Settings.Charge, 20, chargeFactory);

            return builder
                .ConnectToPlayer()
                .WithWeapon()
                .WithClip(chargesPool.Pool)
                .WithWeaponShooting()
                .WithDamage()
                .WithAttackDelay()
                .WithReloadingDelay()
                .WithPrefab(prefab)
                .WithParentInitialize(_parent)
                .WithPositionInitialize(position)
                .Build();
        }

        private readonly Transform _parent;
        private readonly EcsWorld _world;
    }
}