using Assets.Game.Scripts.Levels.Model.Components;
using Assets.Game.Scripts.Levels.Model.Components.Weapons;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsFactory;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents.EntityReference;
using Leopotam.EcsLite;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Factories
{
    internal class WeaponFactory : IEcsFactory<GameObject, GameObject>
    {
        public GameObject Create(
            GameObject prefab,
            Vector3 position,
            Transform parent,
            EcsWorld world)
        {
            var weaponGo = Object.Instantiate(
                prefab,
                position,
                Quaternion.identity,
                parent);

            var weaponEntity = world.NewEntity();

            // Settings
            var weaponSettings = weaponGo.GetComponent<Weapon>().Settings;

            // Weapon
            var weaponPool = world.GetPool<MonoLink<Weapon>>();
            weaponPool.Add(weaponEntity);

            // Transform
            var transformPool = world.GetPool<MonoLink<Transform>>();
            ref var transform = ref transformPool.Add(weaponEntity);
            transform.Value = weaponGo.transform;

            // Collider
            var colliderPool = world.GetPool<MonoLink<Collider>>();
            ref var collider = ref colliderPool.Add(weaponEntity);
            collider.Value = weaponGo.GetComponent<Collider>();

            // Rigidbody
            var rigidbodyPool = world.GetPool<MonoLink<Rigidbody>>();
            ref var rigidbody = ref rigidbodyPool.Add(weaponEntity);
            rigidbody.Value = weaponGo.GetComponent<Rigidbody>();

            // Entity reference
            var entityReference = weaponGo.AddComponent<EntityReference>();
            var entityReferencePool = world.GetPool<MonoLink<EntityReference>>();
            ref var entityReferenceComponent = ref entityReferencePool.Add(weaponEntity);
            entityReference.Pack(weaponEntity);
            entityReferenceComponent.Value = entityReference;

            // Parent transform
            var parentComponentPool = world.GetPool<ParentComponent>();
            ref var parentComponent = ref parentComponentPool.Add(weaponEntity);
            parentComponent.InitParentTransform = parent;
            parentComponent.CurrentParentTransform = parent;

            // Settings

            // Weapon clip component
            var weaponClipPool = world.GetPool<WeaponClipComponent>();
            weaponClipPool.Add(weaponEntity) = new WeaponClipComponent(
                weaponSettings.ChargeType,
                weaponSettings.TotalCharge,
                weaponSettings.CapacityCharge);

            // Damage component
            var damagePool = world.GetPool<DamageComponent>();
            damagePool.Add(weaponEntity) = new DamageComponent
            {
                Damage = weaponSettings.Damage
            };

            // Attack delay component
            var attackDelayPool = world.GetPool<AttackDelayComponent>();
            attackDelayPool.Add(weaponEntity) = new AttackDelayComponent
            {
                Delay = weaponSettings.ShootingDelay
            };

            // Reloading delay component
            var reloadingDelayPool = world.GetPool<ReloadingDelayComponent>();
            reloadingDelayPool.Add(weaponEntity) = new ReloadingDelayComponent
            {
                Delay = weaponSettings.ReloadingTime
            };

            return weaponGo;
        }
    }
}