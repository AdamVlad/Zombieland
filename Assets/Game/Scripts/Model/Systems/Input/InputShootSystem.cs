using System.Runtime.CompilerServices;
using Assets.Game.Scripts.Model.AppData;
using Assets.Game.Scripts.Model.Components;
using Assets.Game.Scripts.Model.Components.Events.Input;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Game.Scripts.Model.Systems.Input
{
    internal sealed class InputShootSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<InputShootComponent>> _inputFilter = default;

        private readonly EcsSharedInject<SharedData> _sharedData;

        public void Run(IEcsSystems systems)
        {
            if (!_sharedData.Value.EventsBus.HasEventSingleton<InputShootEvent>(out var eventBody)) return;

            UnityEngine.Debug.Log($"Shoot {eventBody.ScreenPosition}");

            //SetInputAxis(ref eventBody.Axis);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetInputAxis(ref Vector2 position)
        {
            foreach (var entity in _inputFilter.Value)
            {
                var pools = _inputFilter.Pools;

                ref var moveInput = ref pools.Inc1.Get(entity).MoveInput;
                //moveInput = axis;
            }
        }
    }
}