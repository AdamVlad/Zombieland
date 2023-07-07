using Assets.Game.Scripts.Levels.Model.Components;
using Assets.Game.Scripts.Levels.Model.Components.Delayed;
using Assets.Game.Scripts.Levels.Model.Components.Weapons;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsDelay;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Systems.Player
{
    internal sealed class PlayerAttackSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<PlayerTagComponent,
                BackpackComponent,
                ShootingComponent>,
            Exc<ShootingDelayed,
                ReloadingDelayed>> _playerFilter = default;

        private readonly EcsWorldInject _world = default;

        private readonly EcsPoolInject<DelayedRemove<ShootingDelayed>> _shootingDelayedTimerPool = default;
        private readonly EcsPoolInject<ShootingDelayed> _shootingDelayedPool = default;
        private readonly EcsPoolInject<DelayedRemove<ReloadingDelayed>> _reloadingDelayedTimerPool = default;
        private readonly EcsPoolInject<ReloadingDelayed> _reloadingDelayedPool = default;

        private readonly EcsPoolInject<WeaponClipComponent> _weaponClipPool = default;
        private readonly EcsPoolInject<DamageComponent> _damagePool = default;
        private readonly EcsPoolInject<AttackDelayComponent> _attackDelayPool = default;
        private readonly EcsPoolInject<ReloadingDelayComponent> _reloadingDelayPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var playerEntity in _playerFilter.Value)
            {
                ref var backpackComponent = ref _playerFilter.Get2(playerEntity);
                ref var shootingComponent = ref _playerFilter.Get3(playerEntity);

                if (!shootingComponent.IsShooting) continue;
                if (!backpackComponent.IsWeaponInHand) continue;

                ref var weaponClipComponent = ref _weaponClipPool.Get(backpackComponent.WeaponEntity);
                ref var damageComponent = ref _damagePool.Get(backpackComponent.WeaponEntity);
                ref var attackDelayComponent = ref _attackDelayPool.Get(backpackComponent.WeaponEntity);
                ref var reloadingDelayComponent = ref _reloadingDelayPool.Get(backpackComponent.WeaponEntity);

                if (weaponClipComponent.RestCharge <= 0 && weaponClipComponent.CurrentCharge <= 0) return;

                // Shooting
                Debug.Log($"Shoot with damage {damageComponent.Damage} and current charge = {weaponClipComponent.CurrentCharge} and rest charge = {weaponClipComponent.RestCharge}");
                weaponClipComponent.CurrentCharge--;
                
                // Reloading
                if (weaponClipComponent.CurrentCharge <= 0)
                {
                    var difference = weaponClipComponent.RestCharge - weaponClipComponent.ClipCapacity;
                    weaponClipComponent.CurrentCharge = difference > 0
                        ? weaponClipComponent.ClipCapacity
                        : weaponClipComponent.RestCharge;
                    weaponClipComponent.RestCharge = difference;
                    SetReloadingDelayTime(playerEntity, reloadingDelayComponent.Delay);
                }
                // Shooting delay
                else
                {
                    SetShootingDelayTime(playerEntity, attackDelayComponent.Delay);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetShootingDelayTime(int targetEntity, float time)
        {
            if (_shootingDelayedPool.Has(targetEntity)) return;

            _shootingDelayedPool.Add(targetEntity);

            var delayedEntity = _world.NewEntity();
            ref var timer = ref _shootingDelayedTimerPool.Add(delayedEntity);
            timer.TimeLeft = time;
            timer.Target = _world.PackEntity(targetEntity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetReloadingDelayTime(int targetEntity, float time)
        {
            if (_reloadingDelayedPool.Has(targetEntity)) return;

            _reloadingDelayedPool.Add(targetEntity);

            var delayedEntity = _world.NewEntity();
            ref var timer = ref _reloadingDelayedTimerPool.Add(delayedEntity);
            timer.TimeLeft = time;
            timer.Target = _world.PackEntity(targetEntity);
        }
    }
}