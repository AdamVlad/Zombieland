using Assets.Game.Scripts.Levels.Model.AppData;
using Assets.Game.Scripts.Levels.Model.Components;
using Assets.Game.Scripts.Levels.Model.Components.Delayed;
using Assets.Game.Scripts.Levels.Model.Components.Events.Charges;
using Assets.Game.Scripts.Levels.Model.Components.Weapons.Charges;
using Assets.Game.Scripts.Levels.Model.Services;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;
using DG.Tweening;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Game.Scripts.Levels.Model.Systems.Charges
{
    internal sealed class ChargesReturnToPoolSystem : IEcsRunSystem
    {
        private readonly EcsSharedInject<SharedData> _sharedData = default;

        private readonly EcsPoolInject<StateComponent> _statePool = default;
        private readonly EcsPoolInject<MonoLink<Charge>> _chargePool = default;

        private readonly EcsPoolInject<ReturnToPoolDelayed> _returnToPoolDelayedPool = default;
        private readonly EcsCustomInject<ChargesProviderService> _chargesService = default;

        public void Run(IEcsSystems systems)
        {
            var eventsBus = _sharedData.Value.EventsBus;

            foreach (var eventEntity in eventsBus.GetEventBodies<ChargeReturnToPoolEvent>(out var chargeReturnToPoolEventPool))
            {
                var chargeEntity = chargeReturnToPoolEventPool.Get(eventEntity).Entity;

                _returnToPoolDelayedPool.Del(chargeEntity);

                ref var stateComponent = ref _statePool.Get(chargeEntity);
                ref var charge = ref _chargePool.Get(chargeEntity).Value;

                DOTween.Kill(charge.transform);

                stateComponent.IsActive = false;
                _chargesService.Value.GetPool(charge.Type).Release(charge);
            }
        }
    }
}