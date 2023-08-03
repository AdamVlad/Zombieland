﻿using Assets.Game.Scripts.Levels.Model.Components.Data.Weapons;
using Assets.Game.Scripts.Levels.Model.ScriptableObjects;
using Assets.Game.Scripts.Levels.Model.Services;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;

using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Systems.Weapons
{
    internal sealed class WeaponInitSystem : IEcsInitSystem
    {
        private readonly EcsFilterInject<Inc<MonoLink<Weapon>>> _weaponsFilter = default;
        private readonly EcsPoolInject<WeaponClipComponent> _weaponClipPool = default;
        private readonly EcsPoolInject<WeaponShootingComponent> _weaponShootingPool = default;

        [Inject] private ChargesProviderService _chargesService;
        [Inject] private GameConfigurationSo _gameSettings;

        public void Init(IEcsSystems systems)
        {
            foreach (var spawnEntity in _weaponsFilter.Value)
            {
                ref var weaponClipComponent = ref _weaponClipPool.Get(spawnEntity);
                weaponClipComponent.ChargePool = 
                    _chargesService.GetPool(weaponClipComponent.ChargeType);

                ref var weaponShootingComponent = ref _weaponShootingPool.Get(spawnEntity);
                weaponShootingComponent.ShootingPower =
                    _gameSettings.ShootingPowerDivider - weaponShootingComponent.ShootingPower;
            }
        }
    }
}