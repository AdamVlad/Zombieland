using Assets.Game.Scripts.Levels.Model.Components;
using Assets.Game.Scripts.Levels.Model.Components.Delayed;
using Assets.Game.Scripts.Levels.Model.Components.Events.Charges;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Systems.Charges
{
    internal sealed class ChargesGetFromPoolSystem : IEcsRunSystem
    {
        [Inject] private EventsBus _eventsBus;

        private readonly EcsPoolInject<StateComponent> _statePool = default;
        private readonly EcsPoolInject<LifetimeComponent> _lifetimePool = default;
        private readonly EcsPoolInject<ReturnToPoolDelayed> _returnToPoolDelayedPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var eventEntity in _eventsBus.GetEventBodies<ChargeGetFromPoolEvent>(out var chargeCreatedEventPool))
            {
                ref var eventBody = ref chargeCreatedEventPool.Get(eventEntity);

                _statePool.Get(eventBody.Entity).IsActive = true;
                _lifetimePool.Get(eventBody.Entity).PassedTime = 0;

                _returnToPoolDelayedPool.Add(eventBody.Entity);
            }
        }
    }
}