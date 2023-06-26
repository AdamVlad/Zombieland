using Assets.Game.Scripts.Model.Components;
using Assets.Plugins.IvaLeoEcsLite.UnityEcsComponents;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Runtime.CompilerServices;
using Assets.Plugins.IvaLeoEcsLite.Extensions;
using UnityEngine;
using Assets.Plugins.IvaLib;

namespace Assets.Game.Scripts.Model.Systems.Player
{
    internal sealed class PlayerRotationSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<PlayerTagComponent,
                MonoLink<Transform>,
                RotationComponent,
                MoveComponent,
                ShootingComponent,
                BackpackComponent>> _filter = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var transform = ref _filter.Get2(entity).Value;
                ref var rotationComponent = ref _filter.Get3(entity);
                ref var moveComponent = ref _filter.Get4(entity);
                ref var shootingComponent = ref _filter.Get5(entity);
                ref var backpackComponent = ref _filter.Get6(entity);

                if (!moveComponent.IsMoving && 
                    (!backpackComponent.IsWeaponInHand || !shootingComponent.IsShooting)) continue;

                if (shootingComponent.IsShooting && backpackComponent.IsWeaponInHand)
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
            var position = transform.position;
            if (isSmoothingAlways || IvaMaths.GetAngle180Between(ref position, ref direction) < rotationComponent.SmoothTurningAngle)
            {
                transform.rotation =
                    Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), rotationComponent.Speed * Time.deltaTime);
            }
            else
            {
                transform.LookAt(transform.position + direction);
            }
        }
    }
}