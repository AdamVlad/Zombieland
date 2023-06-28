using System.Runtime.CompilerServices;
using Assets.Game.Scripts.Model.AppData;
using Assets.Game.Scripts.Model.Components;
using Assets.Game.Scripts.Model.Components.Events.Input;
using Assets.Plugins.IvaLib.LeoEcsLite.Extensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Game.Scripts.Model.Systems.Input
{
    internal sealed class InputScreenPositionSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<InputScreenPositionComponent>> _screenFilter = default;

        private readonly EcsSharedInject<SharedData> _sharedData = default;

        public void Run(IEcsSystems systems)
        {
            var eventsBus = _sharedData.Value.EventsBus;

            if (eventsBus.HasEventSingleton<InputShootStartedEvent>(out var shootStartedEventBody))
            {
                SetScreenInputPosition(ref shootStartedEventBody.ScreenPosition);
                return;
            }
            if (eventsBus.HasEventSingleton<InputShootEndedEvent>(out var shootEndedEventBody))
            {
                SetScreenInputPosition(ref shootEndedEventBody.ScreenPosition);
                return;
            }
            if (eventsBus.HasEventSingleton<InputShootDirectionChangedEvent>(out var shootDirectionChangedEventBody))
            {
                SetScreenInputPosition(ref shootDirectionChangedEventBody.ScreenInputPosition);
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