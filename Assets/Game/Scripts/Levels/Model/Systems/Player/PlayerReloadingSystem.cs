﻿using System.Runtime.CompilerServices;

using Assets.Game.Scripts.Levels.Model.Components.Data;
using Assets.Game.Scripts.Levels.Model.Components.Data.Delayed;
using Assets.Game.Scripts.Levels.Model.Components.Data.Events;
using Assets.Game.Scripts.Levels.Model.Components.Data.Weapons;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsDelay;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;

using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Systems.Player
{
    internal sealed class PlayerReloadingSystem : IEcsRunSystem
    {
        [Inject] private readonly EcsWorld _world;
        [Inject] private readonly EventsBus _eventsBus;

        private readonly EcsPoolInject<DelayedRemove<ReloadingDelayed>> _reloadingDelayedTimerPool = default;
        private readonly EcsPoolInject<ReloadingDelayed> _reloadingDelayedPool = default;
        private readonly EcsPoolInject<WeaponClipComponent> _weaponClipPool = default;
        private readonly EcsPoolInject<ReloadingComponent> _reloadingPool = default;

        public void Run(IEcsSystems systems)
        {
            if (!_eventsBus.HasEventSingleton<PlayerReloadingEvent>(out var eventBody)) return;

            ref var weaponClipComponent = ref _weaponClipPool.Get(eventBody.WeaponEntity);
            ref var reloadingDelayComponent = ref _reloadingPool.Get(eventBody.WeaponEntity);

            weaponClipComponent.CurrentCharge = weaponClipComponent.ClipCapacity;

            SetReloadingDelayTime(eventBody.PlayerEntity, reloadingDelayComponent.Delay);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetReloadingDelayTime(int targetEntity, float time)
        {
            if (_reloadingDelayedPool.Has(targetEntity)) return;

            _reloadingDelayedPool.Add(targetEntity);

            var delayedEntity = _world.NewEntity();
            ref var timer = ref _reloadingDelayedTimerPool.Add(delayedEntity);
            timer.TimeLeft = time;
            timer.Target = _world.PackEntity(targetEntity);
        }
    }
}