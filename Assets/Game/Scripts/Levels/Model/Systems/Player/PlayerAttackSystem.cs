using UnityEngine;
using System.Runtime.CompilerServices;
using Assets.Game.Scripts.Levels.Model.Components;
using Assets.Game.Scripts.Levels.Model.Components.Delayed;
using Assets.Game.Scripts.Levels.Model.Components.Weapons;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsDelay;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;
using Assets.Game.Scripts.Levels.Model.AppData;
using Assets.Game.Scripts.Levels.Model.Components.Events;
using Assets.Game.Scripts.Levels.Model.Components.Events.Charges;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using DG.Tweening;

namespace Assets.Game.Scripts.Levels.Model.Systems.Player
{
    internal sealed class PlayerAttackSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<PlayerTagComponent,
                MonoLink<Transform>,
                BackpackComponent,
                ShootingComponent>,
            Exc<ShootingDelayed,
                ReloadingDelayed>> _playerFilter = default;

        private readonly EcsSharedInject<SharedData> _sharedData = default;
        private readonly EcsWorldInject _world = default;

        private readonly EcsPoolInject<DelayedRemove<ShootingDelayed>> _shootingDelayedTimerPool = default;
        private readonly EcsPoolInject<ShootingDelayed> _shootingDelayedPool = default;

        private readonly EcsPoolInject<WeaponClipComponent> _weaponClipPool = default;
        private readonly EcsPoolInject<WeaponShootingComponent> _weaponShootingPool = default;
        private readonly EcsPoolInject<AttackDelayComponent> _attackDelayPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var playerEntity in _playerFilter.Value)
            {
                ref var transformComponent = ref _playerFilter.Get2(playerEntity);
                ref var backpackComponent = ref _playerFilter.Get3(playerEntity);
                ref var shootingComponent = ref _playerFilter.Get4(playerEntity);

                if (!shootingComponent.IsShooting) continue;

                ref var weaponEntity = ref backpackComponent.WeaponEntity;

                ref var weaponClipComponent = ref _weaponClipPool.Get(weaponEntity);
                ref var attackDelayComponent = ref _attackDelayPool.Get(weaponEntity);
                ref var weaponShootingComponent = ref _weaponShootingPool.Get(weaponEntity);

                if (weaponClipComponent.RestChargeCount <= 0 &&
                    weaponClipComponent.CurrentChargeInClipCount <= 0) return;

                var chargeGo = weaponClipComponent.ChargePool.Get();
                _sharedData.Value.EventsBus.NewEvent<ChargeCreatedEvent>() = new ChargeCreatedEvent
                {
                    Entity = chargeGo.Entity
                };

                chargeGo.transform.position = weaponShootingComponent.StartShootingPoint.position;
                chargeGo.transform.DOMove(
                    chargeGo.transform.position +
                    transformComponent.Value.forward * weaponShootingComponent.ShootingDistance,
                    weaponShootingComponent.ShootingPower);

                weaponClipComponent.CurrentChargeInClipCount--;
                
                if (weaponClipComponent.CurrentChargeInClipCount <= 0)
                {
                    ThrowReloadingEvent(playerEntity, weaponEntity);
                }
                else
                {
                    SetShootingDelayTime(playerEntity, attackDelayComponent.Delay);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ThrowReloadingEvent(int playerEntity, int weaponEntity)
        {
            _sharedData.Value.EventsBus.NewEventSingleton<PlayerReloadingEvent>()
                = new PlayerReloadingEvent
                {
                    PlayerEntity = playerEntity,
                    WeaponEntity = weaponEntity
                };
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
    }
}