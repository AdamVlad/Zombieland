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
        private readonly EcsFilterInject<Inc<InputMoveComponent>> _inputFilter = default;

        private readonly EcsSharedInject<SharedData> _sharedData;

        public void Run(IEcsSystems systems)
        {
            if (!_sharedData.Value.EventsBus.HasEventSingleton<InputMoveEvent>(out var eventBody)) return;

            SetInputAxis(ref eventBody.Axis);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetInputAxis(ref Vector2 axis)
        {
            foreach (var entity in _inputFilter.Value)
            {
                var pools = _inputFilter.Pools;

                ref var moveInput = ref pools.Inc1.Get(entity).MoveInput;
                moveInput = axis;
            }
        }
    }
}