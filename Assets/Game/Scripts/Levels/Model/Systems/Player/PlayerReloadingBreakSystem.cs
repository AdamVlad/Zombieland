﻿using Assets.Game.Scripts.Levels.Model.Components;
using Assets.Game.Scripts.Levels.Model.Components.Delayed;
using Assets.Game.Scripts.Levels.Model.Components.Events;
using Assets.Game.Scripts.Levels.Model.Components.Player;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsDelay;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Systems.Player
{
    internal sealed class PlayerReloadingBreakSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject
            <Inc<PlayerTagComponent,
                BackpackComponent>> _playerFilter = default;

        private readonly EcsFilterInject
            <Inc<DelayedRemove<ReloadingDelayed>>> _delayedFilter = default;

        [Inject] private EcsWorld _world;
        [Inject] private EventsBus _eventsBus;

        private readonly EcsPoolInject<ReloadingDelayed> _reloadingDelayedPool = default;

        public void Run(IEcsSystems systems)
        {
            if (!_eventsBus.HasEventSingleton<PlayerPickUpWeaponEvent>(out _)) return;

            foreach (var playerEntity in _playerFilter.Value)
            {
                _reloadingDelayedPool.Del(playerEntity);

                foreach (var delayedEntity in _delayedFilter.Value)
                {
                    ref var delayedComponent = ref _delayedFilter.Get1(delayedEntity);

                    if (!delayedComponent.Target.Unpack(systems.GetWorld(), out var targetEntity)) continue;

                    if (!targetEntity.Equals(playerEntity)) continue;

                    _world.DelEntity(delayedEntity);
                    return;
                }
            }
        }
    }
}