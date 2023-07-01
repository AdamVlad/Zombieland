using Assets.Game.Scripts.Model.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Game.Scripts.Model.Systems
{
    internal sealed class WeaponInitSystem : IEcsInitSystem
    {
        private readonly EcsCustomInject<WeaponsAppearanceService> _weaponsService = default;
        
        public void Init(IEcsSystems systems)
        {
            foreach (var spawnPoint in _weaponsService.Value.WeaponsSpawnPoints)
            {
                _weaponsService.Value.ProvideSpawnPoint(spawnPoint.position);
                _weaponsService.Value.GetWeapon();
            }
        }
    }
}