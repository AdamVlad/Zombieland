using Assets.Game.Scripts.Model.Components;
using Assets.Game.Scripts.Model.Services;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents.EntityReference;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Game.Scripts.Model.Systems.Weapons
{
    internal sealed class WeaponInitSystem : IEcsInitSystem
    {
        private readonly EcsFilterInject<Inc<WeaponSpawnerComponent>> _weaponSpawnersFilter = default;
        private readonly EcsCustomInject<WeaponsAppearanceService> _weaponsService = default;

        public void Init(IEcsSystems systems)
        {
            foreach (var spawnEntity in _weaponSpawnersFilter.Value)
            {
                ref var weaponSpawnerComponent = ref _weaponSpawnersFilter.Get1(spawnEntity);
                var weaponGO = _weaponsService.Value.Get(weaponSpawnerComponent.SpawnPoint);
                var weaponEntityReference = weaponGO.GetComponent<EntityReference>();

                if (weaponEntityReference.Unpack(out var weaponEntity))
                {
                    weaponSpawnerComponent.SpawnedWeaponEntity = weaponEntity;
                }
            }
        }
    }
}