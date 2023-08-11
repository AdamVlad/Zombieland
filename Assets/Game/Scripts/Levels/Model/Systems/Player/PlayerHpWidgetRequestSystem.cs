using System.Runtime.CompilerServices;
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

namespace Assets.Game.Scripts.Levels.Model.Systems.Player
{
    internal sealed class PlayerHpWidgetRequestSystem : IEcsRunSystem
    {
        [Inject] private readonly EventsBus _eventsBus;
        [Inject] private readonly EcsWorld _world;

        private readonly EcsPoolInject<HealthComponent> _healthPool = default;
        private readonly EcsPoolInject<MonoLink<PlayerHpWidget>> _playerHpWidgetPool = default;
        private readonly EcsPoolInject<UpdateWidgetRequest<PlayerHpWidget, float>> 
            _updatePlayerHpWidgetRequestPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var eventEntity in _eventsBus.GetEventBodies<GetDamageEvent>(out var getDamagePool))
            {
                if (!getDamagePool.Get(eventEntity).To.Unpack(_world, out var entity)) continue;
                if (!_healthPool.Has(entity)) continue;

                ref var healthComponent = ref _healthPool.Get(entity);

                if (!_playerHpWidgetPool.Has(entity)) continue;
                if (_updatePlayerHpWidgetRequestPool.Has(entity)) continue;

                _updatePlayerHpWidgetRequestPool.Add(entity) = new UpdateWidgetRequest<PlayerHpWidget, float>
                {
                    Value = healthComponent.CurrentHealth / healthComponent.MaxHealth
                };
            }
        }
    }
}