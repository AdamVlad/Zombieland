using System.Runtime.CompilerServices;
using Assets.Game.Scripts.Levels.Model.Components;
using Assets.Game.Scripts.Levels.Model.Components.Delayed;
using Assets.Game.Scripts.Levels.Model.Components.Weapons;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsDelay;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Game.Scripts.Levels.Model.Components.Events;
using Assets.Game.Scripts.Levels.Model.Components.Events.Charges;
using Assets.Game.Scripts.Levels.Model.Components.Player;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Systems.Player
{
    // Тут вынести в отдельную систему SetAttackDelayTime
    // и отдельную систему, которая задает количество урона пуле
    internal sealed class PlayerAttackSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject
            <Inc<PlayerTagComponent,
                BackpackComponent,
                ShootingComponent>, 
            Exc<AttackDelayed, 
                ReloadingDelayed>> _playerFilter = default;

        [Inject] private EventsBus _eventsBus;
        [Inject] private EcsWorld _world;

        private readonly EcsPoolInject<DelayedRemove<AttackDelayed>> _attackDelayedTimerPool = default;
        private readonly EcsPoolInject<AttackDelayed> _attackDelayedPool = default;

        private readonly EcsPoolInject<WeaponClipComponent> _weaponClipPool = default;
        private readonly EcsPoolInject<AttackDelayComponent> _attackDelayPool = default;
        private readonly EcsPoolInject<DamageComponent> _damagePool = default;

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

                var chargeEntity = weaponClipComponent.ChargePool.Get().Entity;
                ref var weaponDamageComponent = ref _damagePool.Get(weaponEntity);
                ref var chargeDamageComponent = ref _damagePool.Get(chargeEntity);
                chargeDamageComponent.Damage = weaponDamageComponent.Damage;

                _eventsBus.NewEvent<ChargeCreatedEvent>() = new ChargeCreatedEvent
                {
                    Entity = chargeEntity
                };

                weaponClipComponent.CurrentChargeInClipCount--;
                
                if (weaponClipComponent.CurrentChargeInClipCount <= 0)
                {
                    ThrowReloadingEvent(playerEntity, weaponEntity);
                }
                else
                {
                    SetAttackDelayTime(playerEntity, _attackDelayPool.Get(weaponEntity).Delay);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ThrowReloadingEvent(int playerEntity, int weaponEntity)
        {
            _eventsBus.NewEventSingleton<PlayerReloadingEvent>()
                = new PlayerReloadingEvent
                {
                    PlayerEntity = playerEntity,
                    WeaponEntity = weaponEntity
                };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetAttackDelayTime(int targetEntity, float time)
        {
            if (_attackDelayedPool.Has(targetEntity)) return;

            _attackDelayedPool.Add(targetEntity);

            var delayedEntity = _world.NewEntity();
            ref var timer = ref _attackDelayedTimerPool.Add(delayedEntity);
            timer.TimeLeft = time;
            timer.Target = _world.PackEntity(targetEntity);
        }
    }
}