using System.Runtime.CompilerServices;

using Assets.Game.Scripts.Levels.Model.Services;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents.EntityReference;
using Assets.Game.Scripts.Levels.Model.Components.Data;
using Assets.Game.Scripts.Levels.Model.Components.Data.Delayed;
using Assets.Game.Scripts.Levels.Model.Components.Data.Requests;
using Assets.Game.Scripts.Levels.Model.Components.Data.Weapons;

using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Systems.Weapons
{
    internal sealed class WeaponSpawnSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject
            <Inc<WeaponSpawnDelayed,
                WeaponSpawnerComponent>> _weaponSpawnersFilter = default;

        [Inject] private readonly WeaponsProviderService _weaponsService;

        private readonly EcsPoolInject<WeaponSpawnDelayed> _weaponSpawnDelayedPool = default;
        private readonly EcsPoolInject<MonoLink<Collider>> _colliderPool = default;
        private readonly EcsPoolInject<MonoLink<Rigidbody>> _rigidbodyPool = default;
        private readonly EcsPoolInject<WeaponAnimationStartRequest> _weaponAnimationRequestPool = default;
        private readonly EcsPoolInject<WeaponClipComponent> _weaponClipPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var spawnEntity in _weaponSpawnersFilter.Value)
            {
                ref var weaponSpawnerComponent = ref _weaponSpawnersFilter.Get2(spawnEntity);

                var weaponGo = _weaponsService.Get(weaponSpawnerComponent.SpawnPoint);
                if (weaponGo == null) return;

                var weaponEntityReference = weaponGo.GetComponent<EntityReference>();
                if (weaponEntityReference.Unpack(out var weaponEntity))
                {
                    weaponSpawnerComponent.SpawnedWeaponEntity = weaponEntity;
                    _weaponAnimationRequestPool.Add(weaponEntity);

                    SetWeaponParametersDefault(weaponEntity);
                }

                _weaponSpawnDelayedPool.Del(spawnEntity);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetWeaponParametersDefault(int weaponEntity)
        {
            ref var weaponCollider = ref _colliderPool.Get(weaponEntity).Value;
            ref var weaponRigidbody = ref _rigidbodyPool.Get(weaponEntity).Value;
            ref var weaponClip = ref _weaponClipPool.Get(weaponEntity);

            weaponCollider.excludeLayers = LayerMask.GetMask("");
            weaponCollider.isTrigger = true;
            weaponCollider.enabled = true;

            weaponRigidbody.isKinematic = true;

            weaponClip.CurrentChargeInClipCount = weaponClip.ClipCapacity;
            weaponClip.RestChargeCount = weaponClip.TotalCharge - weaponClip.ClipCapacity;
        }
    }
}