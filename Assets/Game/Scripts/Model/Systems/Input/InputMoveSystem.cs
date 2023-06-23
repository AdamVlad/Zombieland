using System.Runtime.CompilerServices;
using Assets.Game.Scripts.Model.AppData;
using Assets.Game.Scripts.Model.Components;
using Assets.Game.Scripts.Model.Components.Events.Input;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Game.Scripts.Model.Systems.Input
{
    internal sealed class InputMoveSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                InputComponent,
                MoveComponent>> _movingFilter = default;

        private readonly EcsSharedInject<SharedData> _sharedData = default;

        public void Run(IEcsSystems systems)
        {
            if (!_sharedData.Value.EventsBus.HasEventSingleton<InputMoveChangedEvent>(out var eventBody)) return;

            SetInputAxis(ref eventBody.Axis);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetInputAxis(ref Vector2 axis)
        {
            var pools = _movingFilter.Pools;

            foreach (var entity in _movingFilter.Value)
            {
                ref var moveInputAxis = ref pools.Inc2.Get(entity).MoveInputAxis;
                moveInputAxis = axis;
            }
        }
    }
}