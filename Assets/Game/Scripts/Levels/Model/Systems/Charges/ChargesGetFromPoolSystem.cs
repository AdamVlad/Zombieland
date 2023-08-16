using Assets.Game.Scripts.Levels.Model.Components.Data;
using Assets.Game.Scripts.Levels.Model.Components.Data.Events.Charges;
using Assets.Game.Scripts.Levels.Model.Components.Data.Processes;
using Assets.Game.Scripts.Levels.Model.Components.Data.Weapons;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsProcess;

using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Systems.Charges
{
    internal sealed class ChargesGetFromPoolSystem : IEcsRunSystem
    {
        [Inject] private readonly EventsBus _eventsBus;

        private readonly EcsPoolInject<WeaponClipComponent> _weaponClipPool = default;
        private readonly EcsPoolInject<LifetimeComponent> _lifetimePool = default;
        private readonly EcsPoolInject<DamageComponent> _damagePool = default;
        private readonly EcsPoolInject<ChargeActiveProcess> _chargeActiveProcessPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var eventEntity in _eventsBus.GetEventBodies<ChargeGetFromPoolEvent>(out var chargeCreatedEventPool))
            {
                ref var eventBody = ref chargeCreatedEventPool.Get(eventEntity);
                ref var weaponClipComponent = ref _weaponClipPool.Get(eventBody.WeaponEntity);

                var chargeEntity = weaponClipComponent.ChargePool.Get().Entity;
                ref var weaponDamageComponent = ref _damagePool.Get(eventBody.WeaponEntity);
                ref var chargeDamageComponent = ref _damagePool.Get(chargeEntity);
                chargeDamageComponent.Damage = weaponDamageComponent.Damage;

                ref var lifeTimeComponent = ref _lifetimePool.Get(chargeEntity);

                _chargeActiveProcessPool.StartNewProcess(chargeEntity, lifeTimeComponent.Lifetime)
                    = new ChargeActiveProcess
                    {
                        PlayerEntity = eventBody.PlayerEntity,
                        WeaponEntity = eventBody.WeaponEntity
                    };
            }
        }
    }
}