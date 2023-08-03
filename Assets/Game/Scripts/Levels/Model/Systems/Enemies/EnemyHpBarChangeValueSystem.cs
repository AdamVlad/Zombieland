using Assets.Game.Scripts.Levels.Model.Components;
using Assets.Game.Scripts.Levels.Model.Components.Events;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Systems.Enemies
{
    internal sealed class EnemyHpBarChangeValueSystem : IEcsRunSystem
    {
        [Inject] private EventsBus _eventsBus;

        private readonly EcsPoolInject<HpBarComponent> _hpBarPool = default;
        private readonly EcsPoolInject<HealthComponent> _healthPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var eventEntity in _eventsBus.GetEventBodies<GetDamageEvent>(out var getDamagePool))
            {
                var entity = getDamagePool.Get(eventEntity).To;

                if (!_hpBarPool.Has(entity)) continue;
                if (!_healthPool.Has(entity)) continue;

                ref var hpBarComponent = ref _hpBarPool.Get(entity);
                ref var healthComponent = ref _healthPool.Get(entity);

                hpBarComponent.Fill.fillAmount = healthComponent.CurrentHealth / healthComponent.MaxHealth;
            }
        }
    }
}