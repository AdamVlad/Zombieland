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
        public WeaponFactory(Transform parent)
        {
            _parent = parent;
        }

        public GameObject Create(
            GameObject prefab,
            Vector3 position,
            EcsWorld world)
        {
            var weaponGo = Object.Instantiate(
                prefab,
                position,
                Quaternion.identity,
                _parent);

            var weaponEntity = world.NewEntity();

            // Settings
            var weapon = weaponGo.GetComponent<Weapon>();

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
            parentComponent.InitParentTransform = _parent;
            parentComponent.CurrentParentTransform = _parent;

            // Settings

            // Weapon clip component
            var weaponClipPool = world.GetPool<WeaponClipComponent>();
            weaponClipPool.Add(weaponEntity) = new WeaponClipComponent(
                weapon.Settings.ChargeType,
                weapon.Settings.TotalCharge,
                weapon.Settings.CapacityCharge);

            // Damage component
            var damagePool = world.GetPool<DamageComponent>();
            damagePool.Add(weaponEntity) = new DamageComponent
            {
                Damage = weapon.Settings.Damage
            };

            // Attack delay component
            var attackDelayPool = world.GetPool<AttackDelayComponent>();
            attackDelayPool.Add(weaponEntity) = new AttackDelayComponent
            {
                Delay = weapon.Settings.ShootingDelay
            };

            // Reloading delay component
            var reloadingPool = world.GetPool<ReloadingComponent>();
            reloadingPool.Add(weaponEntity) = new ReloadingComponent
            {
                Delay = weapon.Settings.ReloadingTime
            };

            // Shooting component
            var shootPointPool = world.GetPool<WeaponShootingComponent>();
            shootPointPool.Add(weaponEntity) = new WeaponShootingComponent
            {
                StartShootingPoint = weapon.ShootPoint,
                ShootingDistance = weapon.Settings.ShootingDistance,
                ShootingPower = weapon.Settings.ShootingPower
            };

            weapon.enabled = false;

            return weaponGo;
        }

        private readonly Transform _parent;
    }
}