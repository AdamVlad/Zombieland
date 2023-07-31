using Assets.Game.Scripts.Levels.Model.Components;
using Assets.Game.Scripts.Levels.Model.Components.Charges;
using Assets.Game.Scripts.Levels.Model.Components.Events.Charges;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsPhysics.Events;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents.EntityReference;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Systems.Charges
{
    internal sealed class ChargesCollisionsSystem : IEcsRunSystem
    {
        [Inject] private EventsBus _eventsBus;

        private readonly EcsFilterInject
            <Inc<ChargeTag,
                StateComponent,
                DamageComponent,
                OnTriggerEnterEvent>> _filter = default;

        private readonly EcsPoolInject<HealthComponent> _healthPool = default;

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

                ref var triggerEnterComponent = ref _filter.Get4(entity);
                if (!triggerEnterComponent.OtherCollider.TryGetComponent<EntityReference>(
                        out var otherEntityReference)) continue;

                if (!otherEntityReference.Unpack(out var otherEntity)) continue;
                if (!_healthPool.Has(otherEntity)) continue;

                ref var healthComponent = ref _healthPool.Get(otherEntity);
                ref var damageComponent = ref _filter.Get3(entity);
                healthComponent.CurrentHealth -= damageComponent.Damage;
            }
        }
    }
}