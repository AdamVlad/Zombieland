using Assets.Game.Scripts.Levels.Model.Components;
using Assets.Game.Scripts.Levels.Model.Components.Enemies;
using Assets.Game.Scripts.Levels.Model.Components.Events;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Systems.Enemies
{
    internal sealed class EnemyHpBarLifetimeSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject
            <Inc<EnemyTagComponent,
                LifetimeComponent>> _filter = default;

        private readonly EcsPoolInject<LifetimeComponent> _lifetimePool = default;

        [Inject] private EventsBus _eventsBus;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var lifetimeComponent = ref _filter.Get2(entity);
                lifetimeComponent.PassedTime -= Time.deltaTime;

                if (lifetimeComponent.PassedTime <= 0)
                {
                    _lifetimePool.Value.Del(entity);

                    _eventsBus.NewEvent<HideEvent>()
                        = new HideEvent
                        {
                            Entity = entity
                        };
                }
            }
        }
    }
}