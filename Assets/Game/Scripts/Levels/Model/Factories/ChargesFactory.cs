using Assets.Game.Scripts.Levels.Model.Components;
using Assets.Game.Scripts.Levels.Model.Components.Weapons.Charges;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsFactory;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsPhysics.Checkers;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents.EntityReference;
using Leopotam.EcsLite;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Factories
{
    internal class ChargesFactory : IEcsFactory<Charge, Charge>
    {
        public ChargesFactory(Transform parent)
        {
            _parent = parent;
        }

        public Charge Create(Charge prefab, Vector3 position, EcsWorld world)
        {
            var chargeGo = Object.Instantiate(prefab, position, Quaternion.identity, _parent);

            var chargeEntity = world.NewEntity();

            // Charge link
            var bulletPool = world.GetPool<MonoLink<Charge>>();
            ref var bulletComponent = ref bulletPool.Add(chargeEntity);
            bulletComponent.Value = chargeGo;

            // Entity reference
            var entityReference = chargeGo.AddComponent<EntityReference>();
            var entityReferencePool = world.GetPool<MonoLink<EntityReference>>();
            ref var entityReferenceComponent = ref entityReferencePool.Add(chargeEntity);
            entityReference.Pack(chargeEntity);
            entityReferenceComponent.Value = entityReference;
            chargeGo.Entity = chargeEntity;

            // Physics events
            chargeGo.AddComponent<OnTriggerEnterChecker>();

            // ChargeTag
            var chargesPool = world.GetPool<ChargeTag>();
            chargesPool.Add(chargeEntity);

            // Transform
            var transformPool = world.GetPool<MonoLink<Transform>>();
            ref var transform = ref transformPool.Add(chargeEntity);
            transform.Value = chargeGo.transform;

            // Collider
            var colliderPool = world.GetPool<MonoLink<Collider>>();
            ref var collider = ref colliderPool.Add(chargeEntity);
            collider.Value = chargeGo.GetComponent<Collider>();

            // State component
            var statePool = world.GetPool<StateComponent>();
            ref var stateComponent = ref statePool.Add(chargeEntity);
            stateComponent.IsActive = false;

            // Lifetime component
            var lifetimePool = world.GetPool<LifetimeComponent>();
            ref var lifetimeComponent = ref lifetimePool.Add(chargeEntity);
            lifetimeComponent.Lifetime = chargeGo.Lifetime;
            lifetimeComponent.PassedTime = 0;

            return chargeGo;
        }

        private readonly Transform _parent;
    }
}