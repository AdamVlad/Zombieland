using Assets.Game.Scripts.Levels.Model.Components;
using Assets.Game.Scripts.Levels.Model.Components.Enemies;
using Assets.Game.Scripts.Levels.Model.Components.Events;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Systems.Enemies
{
    internal sealed class EnemyHpBarHidingSystem : IEcsRunSystem
    {
        [Inject] private EventsBus _eventsBus;

        private readonly EcsPoolInject<HpBarComponent> _hpPool = default;
        private readonly EcsPoolInject<EnemyTagComponent> _enemyPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var eventEntity in _eventsBus.GetEventBodies<HideEvent>(out var hideEventPool))
            {
                var entity = hideEventPool.Get(eventEntity).Entity;

                if (!_enemyPool.Has(entity)) continue;
                if (!_hpPool.Has(entity)) continue;

                ref var hpBarComponent = ref _hpPool.Get(entity);
                hpBarComponent.HpBarCanvas.enabled = false;
            }
        }
    }
}