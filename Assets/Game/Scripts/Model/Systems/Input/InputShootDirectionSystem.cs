using System.Runtime.CompilerServices;
using Assets.Game.Scripts.Model.AppData;
using Assets.Game.Scripts.Model.Components;
using Assets.Game.Scripts.Model.Components.Events.Input;
using Assets.Plugins.IvaLib;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Game.Scripts.Model.Systems.Input
{
    internal sealed class InputShootDirectionSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<InputComponent, ShootingComponent>> _shootingFilter = default;

        private readonly EcsSharedInject<SharedData> _sharedData;

        public void Run(IEcsSystems systems)
        {
            var eventsBus = _sharedData.Value.EventsBus;

            if (eventsBus.HasEventSingleton<InputShootStartedEvent>(out var shootStartedEventBody))
            {
                SetShootDirection(ref shootStartedEventBody.ScreenPosition);
                return;
            }
            if (eventsBus.HasEventSingleton<InputShootEndedEvent>(out var shootEndedEventBody))
            {
                SetShootDirection(ref shootEndedEventBody.ScreenPosition);
                return;
            }
            if (eventsBus.HasEventSingleton<InputShootDirectionChangedEvent>(out var shootDirectionChangedEventBody))
            {
                SetShootDirection(ref shootDirectionChangedEventBody.ScreenClickPosition);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetShootDirection(ref Vector2 screenClickPosition)
        {
            if (!ScreenPointToWorldConverter.GetWorldPointFrom(
                   ref screenClickPosition,
                   _sharedData.Value.MainCamera,
                   _sharedData.Value.RaycastableMask,
                   out var shootDirectionPoint
                   )) return;

            var pools = _shootingFilter.Pools;

            foreach (var entity in _shootingFilter.Value)
            {
                ref var shootingComponent = ref pools.Inc2.Get(entity);

                shootingComponent.DirectionPoint = shootDirectionPoint;
            }
        }
    }
}