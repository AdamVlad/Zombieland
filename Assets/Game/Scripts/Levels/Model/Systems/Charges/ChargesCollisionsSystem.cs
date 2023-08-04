using Assets.Game.Scripts.Levels.Model.Components.Data;
using Assets.Game.Scripts.Levels.Model.Components.Data.Charges;
using Assets.Game.Scripts.Levels.Model.Components.Data.Events;
using Assets.Game.Scripts.Levels.Model.Components.Data.Processes;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsPhysics.Events;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsProcess;
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
            <Inc<ChargeTagComponent,
                DamageComponent,
                OnTriggerEnterEvent,
                Executing<ChargeActiveProcess>>> _filter = default;

        private readonly EcsPoolInject<HealthComponent> _healthPool = default;
        private readonly EcsPoolInject<Process> _processPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var processEntity = _filter.Get4(entity).ProcessEntity;
                _processPool.SetDurationToProcess(processEntity, 0);

                ref var triggerEnterComponent = ref _filter.Get3(entity);
                if (!triggerEnterComponent.OtherCollider.TryGetComponent<EntityReference>(
                        out var otherEntityReference)) continue;

                if (!otherEntityReference.Unpack(out var otherEntity)) continue;
                if (!_healthPool.Has(otherEntity)) continue;

                ref var damageComponent = ref _filter.Get2(entity);

                _eventsBus.NewEvent<GetDamageEvent>()
                    = new GetDamageEvent
                    {
                        From = entity,
                        To = otherEntity,
                        Damage = damageComponent.Damage
                    };
            }
        }
    }
}