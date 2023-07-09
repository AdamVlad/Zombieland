using Assets.Game.Scripts.Levels.Model.AppData;
using Assets.Game.Scripts.Levels.Model.Components;
using Assets.Game.Scripts.Levels.Model.Components.Delayed;
using Assets.Game.Scripts.Levels.Model.Components.Events.Charges;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Game.Scripts.Levels.Model.Systems.Charges
{
    internal sealed class ChargesCreateSystem : IEcsRunSystem
    {
        private readonly EcsSharedInject<SharedData> _sharedData = default;

        private readonly EcsPoolInject<StateComponent> _statePool = default;
        private readonly EcsPoolInject<LifetimeComponent> _lifetimePool = default;
        private readonly EcsPoolInject<ReturnToPoolDelayed> _returnToPoolDelayedPool = default;

        public void Run(IEcsSystems systems)
        {
            var eventsBus = _sharedData.Value.EventsBus;

            foreach (var eventEntity in eventsBus.GetEventBodies<ChargeCreatedEvent>(out var chargeCreatedEventPool))
            {
                ref var eventBody = ref chargeCreatedEventPool.Get(eventEntity);

                _statePool.Get(eventBody.Entity).IsActive = true;
                _lifetimePool.Get(eventBody.Entity).PassedTime = 0;

                _returnToPoolDelayedPool.Add(eventBody.Entity);
            }
        }
    }
}