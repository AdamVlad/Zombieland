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
        [Inject] private readonly EventsBus _eventsBus;
        [Inject] private readonly EcsWorld _world;


        private readonly EcsPoolInject<HealthComponent> _healthPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var eventEntity in _eventsBus.GetEventBodies<GetDamageEvent>(out var getDamagePool))
            {
                ref var getDamageEvent = ref getDamagePool.Get(eventEntity);

                if (!getDamageEvent.To.Unpack(_world, out var entity)) continue;

                if (!_healthPool.Has(entity)) continue;

                ref var healthComponent = ref _healthPool.Get(entity);

                healthComponent.CurrentHealth -= getDamageEvent.Damage;
            }
        }
    }
}