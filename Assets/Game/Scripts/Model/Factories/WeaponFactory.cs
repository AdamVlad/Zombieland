using Assets.Game.Scripts.Model.Components;
using Assets.Game.Scripts.Model.Components.Items;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsFactory;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents.EntityReference;
using Leopotam.EcsLite;
using UnityEngine;

namespace Assets.Game.Scripts.Model.Factories
{
    internal class WeaponFactory : IEcsFactory<GameObject, GameObject>
    {
        public GameObject Create(
            GameObject prefab,
            Vector3 position,
            Transform parent,
            EcsWorld world)
        {
            var weaponGO = Object.Instantiate(
                prefab,
                position,
                Quaternion.identity,
                parent);

            var entityReference = weaponGO.AddComponent<EntityReference>();

            var weaponEntity = world.NewEntity();

            var transformPool = world.GetPool<MonoLink<Transform>>();
            var colliderPool = world.GetPool<MonoLink<Collider>>();
            var rigidbodyPool = world.GetPool<MonoLink<Rigidbody>>();
            var entityReferencePool = world.GetPool<MonoLink<EntityReference>>();
            var weaponComponentPool = world.GetPool<WeaponComponent>();
            var parentComponentPool = world.GetPool<ParentComponent>();

            ref var transform = ref transformPool.Add(weaponEntity);
            transform.Value = weaponGO.transform;

            ref var collider = ref colliderPool.Add(weaponEntity);
            collider.Value = weaponGO.GetComponent<Collider>();

            ref var rigidbody = ref rigidbodyPool.Add(weaponEntity);
            rigidbody.Value = weaponGO.GetComponent<Rigidbody>();

            ref var entityReferenceComponent = ref entityReferencePool.Add(weaponEntity);
            entityReference.Pack(weaponEntity);
            entityReferenceComponent.Value = entityReference;

            ref var parentComponent = ref parentComponentPool.Add(weaponEntity);
            parentComponent.InitParentTransform = parent;
            parentComponent.CurrentParentTransform = parent;

            weaponComponentPool.Add(weaponEntity);

            return weaponGO;
        }
    }
}