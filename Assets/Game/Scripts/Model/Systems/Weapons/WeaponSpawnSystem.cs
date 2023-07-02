using Assets.Game.Scripts.Model.Components;
using Assets.Game.Scripts.Model.Components.Delayed;
using Assets.Game.Scripts.Model.Services;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents.EntityReference;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Game.Scripts.Model.Systems.Weapons
{
    internal sealed class WeaponSpawnSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<WeaponSpawnDelayed,
                WeaponSpawnerComponent>> _weaponSpawnersFilter = default;

        private readonly EcsPoolInject<WeaponSpawnDelayed> _weaponSpawnDelayedPool = default;
        private readonly EcsPoolInject<MonoLink<Collider>> _colliderPool = default;
        private readonly EcsPoolInject<MonoLink<Rigidbody>> _rigidbodyPool = default;
        private readonly EcsCustomInject<WeaponsAppearanceService> _weaponsService = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var spawnEntity in _weaponSpawnersFilter.Value)
            {
                ref var weaponSpawnerComponent = ref _weaponSpawnersFilter.Get2(spawnEntity);

                var weaponGO = _weaponsService.Value.Get(weaponSpawnerComponent.SpawnPoint);
                if (weaponGO == null) return;

                var weaponEntityReference = weaponGO.GetComponent<EntityReference>();
                if (weaponEntityReference.Unpack(out var weaponEntity))
                {
                    weaponSpawnerComponent.SpawnedWeaponEntity = weaponEntity;

                    ref var weaponCollider =
                        ref _colliderPool.Get(weaponEntity).Value;
                    ref var weaponRigidbody =
                        ref _rigidbodyPool.Get(weaponEntity).Value;

                    weaponCollider.enabled = true;
                    weaponRigidbody.isKinematic = true;
                }

                _weaponSpawnDelayedPool.Del(spawnEntity);
            }
        }
    }
}