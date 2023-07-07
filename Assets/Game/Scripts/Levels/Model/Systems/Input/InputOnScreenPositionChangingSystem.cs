﻿using System.Runtime.CompilerServices;
using Assets.Game.Scripts.Levels.Model.AppData;
using Assets.Game.Scripts.Levels.Model.Components;
using Assets.Game.Scripts.Levels.Model.Components.Events.Input;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Systems.Input
{
    internal sealed class InputOnScreenPositionChangingSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<InputScreenPositionComponent>> _screenFilter = default;

        private readonly EcsSharedInject<SharedData> _sharedData = default;

        public void Run(IEcsSystems systems)
        {
            var eventsBus = _sharedData.Value.EventsBus;

            if (eventsBus.HasEventSingleton<InputOnScreenStartedEvent>(out var inputOnScreenStartedEvent))
            {
                SetScreenInputPosition(ref inputOnScreenStartedEvent.ScreenPosition);
                return;
            }
            if (eventsBus.HasEventSingleton<InputOnScreenEndedEvent>(out var inputOnScreenEndedEvent))
            {
                SetScreenInputPosition(ref inputOnScreenEndedEvent.ScreenPosition);
                return;
            }
            if (eventsBus.HasEventSingleton<InputOnScreenPositionChangedEvent>(out var inputOnScreenPositionChangedEvent))
            {
                SetScreenInputPosition(ref inputOnScreenPositionChangedEvent.ScreenInputPosition);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetScreenInputPosition(ref Vector2 screenInputPosition)
        {
            foreach (var entity in _screenFilter.Value)
            {
                ref var inputScreenPositionComponent = ref _screenFilter.Get1(entity);

                inputScreenPositionComponent.Position = screenInputPosition;
            }
        }
    }
}