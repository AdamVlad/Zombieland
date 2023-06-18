using Assets.Game.Scripts.Model.Components;
using Assets.Plugins.IvaLeoEcsLite.UnityEcsComponents;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Game.Scripts.Model.Systems.Player
{
    internal sealed class PlayerRotationSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MonoLink<Transform>, InputMoveComponent, RotationComponent>> _filter = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var pools = _filter.Pools;

                ref var transform = ref pools.Inc1.Get(entity).Value;
                ref var inputMoveComponent = ref pools.Inc2.Get(entity);
                ref var rotationComponent = ref pools.Inc3.Get(entity);

                if (!inputMoveComponent.IsMoveInputStarted) continue;

                ref var moveInput = ref inputMoveComponent.MoveInput;
                var direction = (Vector3.forward * moveInput.y + Vector3.right * moveInput.x).normalized;
                var targetRotation = Quaternion.LookRotation(direction);

                if (Quaternion.Angle(transform.rotation, targetRotation) < rotationComponent.SmoothTurningAngle)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationComponent.Speed * Time.deltaTime);
                }
                else
                {
                    transform.LookAt(transform.position + direction);
                }
            }
        }
    }
}