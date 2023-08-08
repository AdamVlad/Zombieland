using Assets.Game.Scripts.Levels.Model.Components.Data;
using Assets.Game.Scripts.Levels.Model.Components.Data.Events;
using Assets.Game.Scripts.Levels.Model.Components.Data.Requests;
using Assets.Game.Scripts.Levels.View.Widgets;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;

using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Systems.Enemies
{
    internal sealed class EnemyHpWidgetsUpdateRequestSystem : IEcsRunSystem
    {
        [Inject] private readonly EventsBus _eventsBus;
        [Inject] private readonly EcsWorld _world;

        private readonly EcsPoolInject<HealthComponent> _healthPool = default;
        private readonly EcsPoolInject<MonoLink<EnemyHpWidget>> _enemyHpWidgetPool = default;
        private readonly EcsPoolInject<UpdateWidgetRequest<EnemyHpWidget, float>> _updateEnemyHpWidgetRequestPool =
            default;

        public void Run(IEcsSystems systems)
        {
            foreach (var eventEntity in _eventsBus.GetEventBodies<GetDamageEvent>(out var getDamagePool))
            {
                if (!getDamagePool.Get(eventEntity).To.Unpack(_world, out var entity)) continue;
                if (!_healthPool.Has(entity)) continue;

                ref var healthComponent = ref _healthPool.Get(entity);

                if (!_enemyHpWidgetPool.Has(entity)) continue;
                if (_updateEnemyHpWidgetRequestPool.Has(entity)) continue;

                _updateEnemyHpWidgetRequestPool.Add(entity) = new UpdateWidgetRequest<EnemyHpWidget, float>
                {
                    Value = healthComponent.CurrentHealth / healthComponent.MaxHealth
                };
            }
        }
    }
}