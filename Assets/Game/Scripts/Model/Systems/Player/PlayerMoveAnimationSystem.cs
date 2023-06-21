using Assets.Game.Scripts.Model.AppData;
using Assets.Game.Scripts.Model.Components;
using Assets.Plugins.IvaLeoEcsLite.UnityEcsComponents;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Assets.Game.Scripts.Model.Systems.Player
{
    internal sealed class PlayerMoveAnimationSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<
            MonoLink<Animator>,
            MonoLink<Transform>,
            MoveComponent,
            BackpackComponent,
            ShootingComponent>> _filter = default;

        private readonly EcsSharedInject<SharedData> _sharedData = default;

        public void Run(IEcsSystems systems)
        {
            var pools = _filter.Pools;

            foreach (var entity in _filter.Value)
            {
                ref var animator = ref pools.Inc1.Get(entity).Value;
                ref var transform = ref pools.Inc2.Get(entity).Value;
                ref var moveComponent = ref pools.Inc3.Get(entity);
                ref var weaponEntity = ref pools.Inc4.Get(entity).WeaponEntity;
                ref var shootingComponent = ref pools.Inc5.Get(entity);

                ref var moveInputAxis = ref moveComponent.MoveInputAxis;

                animator.SetBool("WeaponInHand", weaponEntity != -1);

                if (shootingComponent.IsShooting)
                {
                    var rotationAngle = GetRotationAngle(ref shootingComponent.Direction, transform.rotation);

                    Debug.Log($"Angle = {rotationAngle}");

                    var axisXInShiftingSystemCoordinate =
                        moveInputAxis.x * Mathf.Cos(rotationAngle) +
                        moveInputAxis.y * Mathf.Sin(rotationAngle);

                    var axisYInShiftingSystemCoordinate =
                        moveInputAxis.y * Mathf.Cos(rotationAngle) -
                        moveInputAxis.x * Mathf.Sin(rotationAngle);

                    animator.SetFloat("x", axisXInShiftingSystemCoordinate);
                    animator.SetFloat("y", axisYInShiftingSystemCoordinate);

                    var moveDirection = new Vector3(axisXInShiftingSystemCoordinate, 0, axisYInShiftingSystemCoordinate).normalized;

                    Debug.DrawRay(transform.position, moveDirection * 10, Color.yellow);
                }
                else
                {
                    animator.SetFloat("x", 0);
                    animator.SetFloat("y", Mathf.Sqrt(moveInputAxis.x * moveInputAxis.x + moveInputAxis.y * moveInputAxis.y));
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private float GetRotationAngle(ref Vector3 direction, Quaternion ownRotation)
        {
            var targetRotation = Quaternion.LookRotation(direction);
            var angle = Quaternion.Angle(ownRotation, targetRotation);

            return direction.x > 0 ? 360 - angle : angle;
        }
    }
}