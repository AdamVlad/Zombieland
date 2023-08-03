using Assets.Game.Scripts.Levels.Model.Components.Data;
using Assets.Game.Scripts.Levels.Model.Components.Data.Charges;
using Assets.Game.Scripts.Levels.Model.Components.Data.Delayed;
using Assets.Game.Scripts.Levels.Model.Components.Data.Events.Charges;
using Assets.Game.Scripts.Levels.Model.Services;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;
using DG.Tweening;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Systems.Charges
{
    internal sealed class ChargesReturnToPoolSystem : IEcsRunSystem
    {
        [Inject] private EventsBus _eventsBus;
        [Inject] private ChargesProviderService _chargesService;

        private readonly EcsPoolInject<StateComponent> _statePool = default;
        private readonly EcsPoolInject<MonoLink<Charge>> _chargePool = default;
        private readonly EcsPoolInject<ReturnToPoolDelayed> _returnToPoolDelayedPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var eventEntity in _eventsBus.GetEventBodies<ChargeReturnToPoolEvent>(out var chargeReturnToPoolEventPool))
            {
                var chargeEntity = chargeReturnToPoolEventPool.Get(eventEntity).Entity;

                _returnToPoolDelayedPool.Del(chargeEntity);

                ref var stateComponent = ref _statePool.Get(chargeEntity);
                if (!stateComponent.IsActive) continue;

                ref var charge = ref _chargePool.Get(chargeEntity).Value;

                DOTween.Kill(charge.transform);

                _chargesService.GetPool(charge.Type).Release(charge);
                stateComponent.IsActive = false;
            }
        }
    }
}