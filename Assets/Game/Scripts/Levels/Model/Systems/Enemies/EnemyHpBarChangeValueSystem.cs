using Assets.Game.Scripts.Levels.Model.Components.Data;
using Assets.Game.Scripts.Levels.Model.Components.Data.Events;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;

using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Systems.Enemies
{
    internal sealed class EnemyHpBarChangeValueSystem : IEcsRunSystem
    {
        [Inject] private readonly EventsBus _eventsBus;
        [Inject] private readonly EcsWorld _world;

        private readonly EcsPoolInject<HpBarComponent> _hpBarPool = default;
        private readonly EcsPoolInject<HealthComponent> _healthPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var eventEntity in _eventsBus.GetEventBodies<GetDamageEvent>(out var getDamagePool))
            {
                var entityPacked = getDamagePool.Get(eventEntity).To;

                if (!entityPacked.Unpack(_world, out var entity)) continue;
                if (!_hpBarPool.Has(entity)) continue;
                if (!_healthPool.Has(entity)) continue;

                ref var hpBarComponent = ref _hpBarPool.Get(entity);
                ref var healthComponent = ref _healthPool.Get(entity);

                hpBarComponent.Fill.fillAmount = healthComponent.CurrentHealth / healthComponent.MaxHealth;
            }
        }
    }
}