using Assets.Game.Scripts.Levels.Model.Components.Data;
using Assets.Game.Scripts.Levels.Model.Components.Data.Events;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;

using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Systems
{
    internal sealed class GetDamageSystem : IEcsRunSystem
    {
        [Inject] private EventsBus _eventsBus;

        private readonly EcsPoolInject<HealthComponent> _healthPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var eventEntity in _eventsBus.GetEventBodies<GetDamageEvent>(out var getDamageEventPool))
            {
                var getDamageEvent = getDamageEventPool.Get(eventEntity);

                if (!_healthPool.Has(getDamageEvent.To)) continue;

                ref var healthComponent = ref _healthPool.Get(getDamageEvent.To);

                healthComponent.CurrentHealth -= getDamageEvent.Damage;
                if (healthComponent.CurrentHealth <= 0)
                {
                    // Уничтожить или вернуть в пул
                }
            }
        }
    }
}