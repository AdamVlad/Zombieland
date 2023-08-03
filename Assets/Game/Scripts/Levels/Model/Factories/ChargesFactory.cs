using Assets.Game.Scripts.Levels.Model.Components;
using Assets.Game.Scripts.Levels.Model.Components.Data;
using Assets.Game.Scripts.Levels.Model.Components.Data.Charges;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsPhysics.Checkers;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents.EntityReference;
using Assets.Plugins.IvaLib.UnityLib.Factory;
using Leopotam.EcsLite;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Factories
{
    internal class ChargesFactory : IFactory<Charge, Charge>
    {
        public ChargesFactory(EcsWorld world, Transform parent = null)
        {
            _world = world;
            _parent = parent;
        }

        public Charge Create(Charge prefab, Vector3 position)
        {
            var chargeGo = Object.Instantiate(prefab, position, Quaternion.identity, _parent);

            var chargeEntity = _world.NewEntity();

            // Charge link
            var bulletPool = _world.GetPool<MonoLink<Charge>>();
            ref var bulletComponent = ref bulletPool.Add(chargeEntity);
            bulletComponent.Value = chargeGo;

            // Entity reference
            var entityReference = chargeGo.AddComponent<EntityReference>();
            var entityReferencePool = _world.GetPool<MonoLink<EntityReference>>();
            ref var entityReferenceComponent = ref entityReferencePool.Add(chargeEntity);
            entityReference.Pack(chargeEntity);
            entityReferenceComponent.Value = entityReference;
            chargeGo.Entity = chargeEntity;

            // Physics events
            chargeGo.AddComponent<OnTriggerEnterChecker>();

            // ChargeTag
            var chargesPool = _world.GetPool<ChargeTag>();
            chargesPool.Add(chargeEntity);

            // Transform
            var transformPool = _world.GetPool<MonoLink<Transform>>();
            ref var transform = ref transformPool.Add(chargeEntity);
            transform.Value = chargeGo.transform;

            // Collider
            var colliderPool = _world.GetPool<MonoLink<Collider>>();
            ref var collider = ref colliderPool.Add(chargeEntity);
            collider.Value = chargeGo.GetComponent<Collider>();

            // State component
            var statePool = _world.GetPool<StateComponent>();
            ref var stateComponent = ref statePool.Add(chargeEntity);
            stateComponent.IsActive = false;

            // Damage
            var damagePool = _world.GetPool<DamageComponent>();
            damagePool.Add(chargeEntity);

            // Lifetime component
            var lifetimePool = _world.GetPool<LifetimeComponent>();
            ref var lifetimeComponent = ref lifetimePool.Add(chargeEntity);
            lifetimeComponent.Lifetime = chargeGo.Lifetime;
            lifetimeComponent.PassedTime = 0;

            return chargeGo;
        }

        private readonly Transform _parent;
        private readonly EcsWorld _world;
    }
}