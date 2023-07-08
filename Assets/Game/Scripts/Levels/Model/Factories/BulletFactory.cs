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
    internal class BulletFactory : IEcsFactory<Bullet, Bullet>
    {
        public BulletFactory(Transform parent)
        {
            _parent = parent;
        }

        public Bullet Create(Bullet prefab, Vector3 position, EcsWorld world)
        {
            var bulletGo = Object.Instantiate(prefab, position, Quaternion.identity, _parent);

            var bulletEntity = world.NewEntity();

            // Entity reference
            var entityReference = bulletGo.AddComponent<EntityReference>();
            var entityReferencePool = world.GetPool<MonoLink<EntityReference>>();
            ref var entityReferenceComponent = ref entityReferencePool.Add(bulletEntity);
            entityReference.Pack(bulletEntity);
            entityReferenceComponent.Value = entityReference;

            // Physics events
            bulletGo.AddComponent<OnTriggerEnterChecker>();

            // ChargeTag
            var chargesPool = world.GetPool<ChargeTag>();
            chargesPool.Add(bulletEntity);

            // Transform
            var transformPool = world.GetPool<MonoLink<Transform>>();
            ref var transform = ref transformPool.Add(bulletEntity);
            transform.Value = bulletGo.transform;

            // Collider
            var colliderPool = world.GetPool<MonoLink<Collider>>();
            ref var collider = ref colliderPool.Add(bulletEntity);
            collider.Value = bulletGo.GetComponent<Collider>();

            return bulletGo;
        }

        private readonly Transform _parent;
    }
}