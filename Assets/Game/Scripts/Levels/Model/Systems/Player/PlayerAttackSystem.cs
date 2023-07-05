using Assets.Game.Scripts.Levels.Model.Components;
using Assets.Game.Scripts.Levels.Model.Components.Weapons;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Systems.Player
{
    internal sealed class PlayerAttackSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<PlayerTagComponent,
                BackpackComponent,
                ShootingComponent>> _playerFilter = default;

        private readonly EcsPoolInject<WeaponComponent> _weaponPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var playerEntity in _playerFilter.Value)
            {
                ref var backpackComponent = ref _playerFilter.Get2(playerEntity);
                ref var shootingComponent = ref _playerFilter.Get3(playerEntity);

                if (!shootingComponent.IsShooting) continue;
                if (!backpackComponent.IsWeaponInHand) continue;

                ref var weaponComponent = ref _weaponPool.Get(backpackComponent.WeaponEntity);
                Debug.Log("Shoot");
            }
        }
    }
}