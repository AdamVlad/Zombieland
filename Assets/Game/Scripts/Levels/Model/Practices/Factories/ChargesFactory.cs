using Assets.Game.Scripts.Levels.Model.Components.Data.Charges;
using Assets.Game.Scripts.Levels.Model.Practices.Builders.Context;
using Assets.Game.Scripts.Levels.Model.Practices.Builders;
using Assets.Plugins.IvaLib.UnityLib.Factory;

using Leopotam.EcsLite;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Practices.Factories
{
    internal class ChargesFactory : IFactory<Charge, Charge>
    {
        public ChargesFactory(EcsWorld world, Transform parent = null)
        {
            _world = world;
            _parent = parent;
        }

        public Charge Create(Charge prefab, Vector3 position = default)
        {
            var builder = new ChargeBuilder(new EcsContext(_world));

            return builder
                .WithCharge()
                .WithTag()
                .WithDamage()
                .WithLifetime()
                .WithPrefab(prefab)
                .WithPositionInitialize(position)
                .WithParentInitialize(_parent)
                .WithTransform()
                .WithCollider()
                .WithEntityReference()
                .Build();
        }

        private readonly Transform _parent;
        private readonly EcsWorld _world;
    }
}