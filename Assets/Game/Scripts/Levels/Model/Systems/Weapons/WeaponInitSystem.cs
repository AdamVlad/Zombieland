using Assets.Game.Scripts.Levels.Model.Components;
using Assets.Game.Scripts.Levels.Model.Components.Requests;
using Assets.Game.Scripts.Levels.Model.Services;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents.EntityReference;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Game.Scripts.Levels.Model.Systems.Weapons
{
    internal sealed class WeaponInitSystem : IEcsInitSystem
    {
        private readonly EcsFilterInject<Inc<WeaponSpawnerComponent>> _weaponSpawnersFilter = default;
        private readonly EcsCustomInject<WeaponsProviderService> _weaponsService = default;

        private readonly EcsPoolInject<WeaponAnimationStartRequest> _weaponAnimationRequestPool = default;

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
                    _weaponAnimationRequestPool.Add(weaponEntity);
                }
            }
        }
    }
}