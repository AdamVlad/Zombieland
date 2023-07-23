using System.Runtime.CompilerServices;
using Assets.Game.Scripts.Levels.Model.Components;
using Assets.Game.Scripts.Levels.Model.Components.Events.Input;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Systems.Input
{
    internal sealed class InputMoveSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject
            <Inc<InputComponent,
                MoveComponent>> _movingFilter = default;

        [Inject] private EventsBus _eventsBus;

        public void Run(IEcsSystems systems)
        {
            if (!_eventsBus.HasEventSingleton<InputMoveChangedEvent>(out var eventBody)) return;

            SetInputAxis(ref eventBody.Axis);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetInputAxis(ref Vector2 axis)
        {
            foreach (var entity in _movingFilter.Value)
            {
                ref var moveInputAxis = ref _movingFilter.Get2(entity).MoveInputAxis;
                moveInputAxis = axis;
            }
        }
    }
}