using Assets.Game.Scripts.Levels.Model.AppData;
using Assets.Game.Scripts.Levels.Model.Components;
using Assets.Game.Scripts.Levels.Model.Components.Charges;
using Assets.Game.Scripts.Levels.Model.Components.Events.Charges;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsPhysics.Events;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Game.Scripts.Levels.Model.Systems.Charges
{
    internal sealed class ChargesCollisionsSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<ChargeTag,
                StateComponent,
                OnTriggerEnterEvent>> _filter = default;

        private readonly EcsSharedInject<SharedData> _sharedData = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var stateComponent = ref _filter.Get2(entity);

                if (!stateComponent.IsActive) continue;

                _sharedData.Value.EventsBus.NewEvent<ChargeReturnToPoolEvent>()
                    = new ChargeReturnToPoolEvent
                    {
                        Entity = entity
                    };
            }
        }
    }
}