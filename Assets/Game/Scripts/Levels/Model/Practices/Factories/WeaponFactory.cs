using Assets.Game.Scripts.Levels.Model.Components.Data;
using Assets.Game.Scripts.Levels.Model.Components.Data.Weapons;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents.EntityReference;
using Assets.Plugins.IvaLib.UnityLib.Factory;

using Leopotam.EcsLite;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Practices.Factories
{
    internal class WeaponFactory : IFactory<GameObject, GameObject>
    {
        public WeaponFactory(EcsWorld world, Transform parent = null)
        {
            _world = world;
            _parent = parent;
        }

        public GameObject Create(
            GameObject prefab,
            Vector3 position)
        {
            var weaponGo = Object.Instantiate(
                prefab,
                position,
                Quaternion.identity,
                _parent);

            var weaponEntity = _world.NewEntity();

            // Settings
            var weapon = weaponGo.GetComponent<Weapon>();

            // Weapon
            var weaponPool = _world.GetPool<MonoLink<Weapon>>();
            weaponPool.Add(weaponEntity);

            // Transform
            var transformPool = _world.GetPool<MonoLink<Transform>>();
            ref var transform = ref transformPool.Add(weaponEntity);
            transform.Value = weaponGo.transform;

            // Collider
            var colliderPool = _world.GetPool<MonoLink<Collider>>();
            ref var collider = ref colliderPool.Add(weaponEntity);
            collider.Value = weaponGo.GetComponent<Collider>();

            // Rigidbody
            var rigidbodyPool = _world.GetPool<MonoLink<Rigidbody>>();
            ref var rigidbody = ref rigidbodyPool.Add(weaponEntity);
            rigidbody.Value = weaponGo.GetComponent<Rigidbody>();

            // Entity reference
            var entityReference = weaponGo.AddComponent<EntityReference>();
            var entityReferencePool = _world.GetPool<MonoLink<EntityReference>>();
            ref var entityReferenceComponent = ref entityReferencePool.Add(weaponEntity);
            entityReference.Pack(weaponEntity);
            entityReferenceComponent.Value = entityReference;

            // Parent transform
            var parentComponentPool = _world.GetPool<ParentComponent>();
            ref var parentComponent = ref parentComponentPool.Add(weaponEntity);
            parentComponent.InitParentTransform = _parent;
            parentComponent.CurrentParentTransform = _parent;

            // Settings

            // Weapon clip component
            var weaponClipPool = _world.GetPool<WeaponClipComponent>();
            weaponClipPool.Add(weaponEntity) = new WeaponClipComponent(
                weapon.Settings.ChargeType,
                weapon.Settings.TotalCharge,
                weapon.Settings.CapacityCharge);

            // Damage component
            var damagePool = _world.GetPool<DamageComponent>();
            damagePool.Add(weaponEntity) = new DamageComponent
            {
                Damage = weapon.Settings.Damage
            };

            // Attack delay component
            var attackDelayPool = _world.GetPool<AttackDelayComponent>();
            attackDelayPool.Add(weaponEntity) = new AttackDelayComponent
            {
                Delay = weapon.Settings.ShootingDelay
            };

            // Reloading delay component
            var reloadingPool = _world.GetPool<ReloadingComponent>();
            reloadingPool.Add(weaponEntity) = new ReloadingComponent
            {
                Delay = weapon.Settings.ReloadingTime
            };

            // Shooting component
            var shootPointPool = _world.GetPool<WeaponShootingComponent>();
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
        private readonly EcsWorld _world;
    }
}