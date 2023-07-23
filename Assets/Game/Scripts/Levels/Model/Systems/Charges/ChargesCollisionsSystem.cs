using Assets.Game.Scripts.Levels.Model.Components;
using Assets.Game.Scripts.Levels.Model.Components.Charges;
using Assets.Game.Scripts.Levels.Model.Components.Events.Charges;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsPhysics.Events;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Systems.Charges
{
    internal sealed class ChargesCollisionsSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject
            <Inc<ChargeTag,
                StateComponent,
                OnTriggerEnterEvent>> _filter = default;

        [Inject] private EventsBus _eventsBus;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var stateComponent = ref _filter.Get2(entity);

                if (!stateComponent.IsActive) continue;

                _eventsBus.NewEvent<ChargeReturnToPoolEvent>()
                    = new ChargeReturnToPoolEvent
                    {
                        Entity = entity
                    };
            }
        }
    }
}