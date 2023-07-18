using System.Runtime.CompilerServices;
using Assets.Game.Scripts.Levels.Model.Components;
using Assets.Game.Scripts.Levels.Model.Components.Delayed;
using Assets.Game.Scripts.Levels.Model.Components.Weapons;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsDelay;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Game.Scripts.Levels.Model.AppData;
using Assets.Game.Scripts.Levels.Model.Components.Events;
using Assets.Game.Scripts.Levels.Model.Components.Events.Charges;
using Assets.Game.Scripts.Levels.Model.Components.Player;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

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

        private readonly EcsSharedInject<SharedData> _sharedData = default;
        private readonly EcsWorldInject _world = default;

        private readonly EcsPoolInject<DelayedRemove<ShootingDelayed>> _shootingDelayedTimerPool = default;
        private readonly EcsPoolInject<ShootingDelayed> _shootingDelayedPool = default;

        private readonly EcsPoolInject<WeaponClipComponent> _weaponClipPool = default;
        private readonly EcsPoolInject<AttackDelayComponent> _attackDelayPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var playerEntity in _playerFilter.Value)
            {
                ref var shootingComponent = ref _playerFilter.Get3(playerEntity);
                if (!shootingComponent.IsShooting) continue;

                ref var weaponEntity = ref _playerFilter.Get2(playerEntity).WeaponEntity;

                ref var weaponClipComponent = ref _weaponClipPool.Get(weaponEntity);

                if (weaponClipComponent.RestChargeCount <= 0 &&
                    weaponClipComponent.CurrentChargeInClipCount <= 0) return;

                _sharedData.Value.EventsBus.NewEvent<ChargeCreatedEvent>() = new ChargeCreatedEvent
                {
                    Entity = weaponClipComponent.ChargePool.Get().Entity
                };

                weaponClipComponent.CurrentChargeInClipCount--;
                
                if (weaponClipComponent.CurrentChargeInClipCount <= 0)
                {
                    ThrowReloadingEvent(playerEntity, weaponEntity);
                }
                else
                {
                    SetShootingDelayTime(playerEntity, _attackDelayPool.Get(weaponEntity).Delay);
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