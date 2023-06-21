using Assets.Game.Scripts.Model.Components;
using Assets.Plugins.IvaLeoEcsLite.UnityEcsComponents;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Assets.Game.Scripts.Model.Systems.Player
{
    internal sealed class PlayerRotationSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<
            PlayerTagComponent,
            MonoLink<Transform>,
            RotationComponent,
            MoveComponent,
            ShootingComponent>> _filter = default;

        public void Run(IEcsSystems systems)
        {
            var pools = _filter.Pools;

            foreach (var entity in _filter.Value)
            {
                ref var transform = ref pools.Inc2.Get(entity).Value;
                ref var rotationComponent = ref pools.Inc3.Get(entity);
                ref var moveComponent = ref pools.Inc4.Get(entity);
                ref var shootingComponent = ref pools.Inc5.Get(entity);

                if (!moveComponent.IsMoving && !shootingComponent.IsShooting) continue;

                if (shootingComponent.IsShooting)
                {
                    RotateInShootingDirection(ref shootingComponent, ref transform, ref rotationComponent);
                }
                else
                {
                    RotateInMoveDirection(ref moveComponent, ref transform, ref rotationComponent);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void RotateInMoveDirection(
            ref MoveComponent moveComponent,
            ref Transform transform,
            ref RotationComponent rotationComponent)
        {
            var direction = 
                (Vector3.forward * moveComponent.MoveInputAxis.y + Vector3.right * moveComponent.MoveInputAxis.x).normalized;
            RotateByDirection(ref direction, ref transform, ref rotationComponent, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void RotateInShootingDirection(
            ref ShootingComponent shootingComponent,
            ref Transform transform,
            ref RotationComponent rotationComponent)
        {
            RotateByDirection(ref shootingComponent.Direction, ref transform, ref rotationComponent, true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void RotateByDirection(
            ref Vector3 direction,
            ref Transform transform,
            ref RotationComponent rotationComponent, 
            bool isSmoothingAlways)
        {
            var targetRotation = Quaternion.LookRotation(direction);

            if (isSmoothingAlways || Quaternion.Angle(transform.rotation, targetRotation) < rotationComponent.SmoothTurningAngle)
            {
                transform.rotation =
                    Quaternion.Lerp(transform.rotation, targetRotation, rotationComponent.Speed * Time.deltaTime);
            }
            else
            {
                transform.LookAt(transform.position + direction);
            }
        }
    }
}