using Assets.Game.Scripts.Levels.Model.Components.Data;
using Assets.Game.Scripts.Levels.Model.Components.Data.Events;
using Assets.Game.Scripts.Levels.Model.Components.Data.Requests;
using Assets.Game.Scripts.Levels.View.Widgets;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;

using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Runtime.CompilerServices;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Systems
{
    internal sealed class GetDamageSystem : IEcsRunSystem
    {
        [Inject] private readonly EventsBus _eventsBus;
        [Inject] private readonly EcsWorld _world;

        private readonly EcsPoolInject<HealthComponent> _healthPool = default;
        private readonly EcsPoolInject<MonoLink<PlayerHpWidget>> _playerHpWidgetPool = default;
        private readonly EcsPoolInject<UpdateWidgetRequest<PlayerHpWidget, float>> _updateWidgetRequestPool = default;

        public void Run(IEcsSystems systems)
        {
            var eventFilter = _eventsBus.GetEventBodies<GetDamageEvent>(out var getDamagePool);

            ReduceHealth(eventFilter, ref getDamagePool);

            SetHpWidgetUpdateRequest(eventFilter, ref getDamagePool);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ReduceHealth(EcsFilter eventFilter, ref EcsPool<GetDamageEvent> getDamagePool)
        {
            foreach (var eventEntity in eventFilter)
            {
                ref var getDamageEvent = ref getDamagePool.Get(eventEntity);

                if (!getDamageEvent.To.Unpack(_world, out var entity)) continue;

                if (!_healthPool.Has(entity)) continue;

                ref var healthComponent = ref _healthPool.Get(entity);

                healthComponent.CurrentHealth -= getDamageEvent.Damage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetHpWidgetUpdateRequest(EcsFilter eventFilter, ref EcsPool<GetDamageEvent> getDamagePool)
        {
            foreach (var eventEntity in eventFilter)
            {
                ref var getDamageEvent = ref getDamagePool.Get(eventEntity);

                if (!getDamageEvent.To.Unpack(_world, out var entity)) continue;

                if (!_healthPool.Has(entity)) continue;

                ref var healthComponent = ref _healthPool.Get(entity);

                if (!_playerHpWidgetPool.Has(entity)) continue;
                if (_updateWidgetRequestPool.Has(entity)) continue;

                _updateWidgetRequestPool.Add(entity) = new UpdateWidgetRequest<PlayerHpWidget, float>
                {
                    Value = healthComponent.CurrentHealth / healthComponent.MaxHealth
                };
            }
        }
    }
}