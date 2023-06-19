using Assets.Game.Scripts.Model.Components;
using Assets.Plugins.IvaLeoEcsLite.UnityEcsComponents;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System;
using UnityEngine;

namespace Assets.Game.Scripts.Model.Systems.Player
{
    internal sealed class PlayerMoveAnimationSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MonoLink<Animator>, MoveComponent, BackpackComponent, ShootingComponent>> _filter = default;

        public void Run(IEcsSystems systems)
        {
            var pools = _filter.Pools;

            foreach (var entity in _filter.Value)
            {
                ref var animator = ref pools.Inc1.Get(entity).Value;
                ref var moveInputAxis = ref pools.Inc2.Get(entity).MoveInputAxis;
                ref var weaponEntity = ref pools.Inc3.Get(entity).WeaponEntity;
                ref var shootingComponent = ref pools.Inc4.Get(entity);

                animator.SetBool("WeaponInHand", weaponEntity != -1);
                if (shootingComponent.IsShooting)
                {
                    //var inputAngleByY = GetAngleByTan(moveInputAxis.x, moveInputAxis.y);


                    //Debug.Log("Not Shift = " + inputAngleByY);

                    //Debug.Log("Shift = " + shiftInputAngleByY);

                    //var angleByShiftSystem = inputAngleByY + shiftInputAngleByY - 90;

                    //Console.WriteLine("The previous tangent is equivalent to {0} degrees.", angle);


                    animator.SetFloat("x", moveInputAxis.x);
                    animator.SetFloat("y", moveInputAxis.y);
                }
                else
                {
                    animator.SetFloat("x", 0);
                    animator.SetFloat("y", Mathf.Sqrt(moveInputAxis.x * moveInputAxis.x + moveInputAxis.y * moveInputAxis.y));
                }
            }
        }

        private double GetAngleByTan(float x, float y)
        {
            var radians = Math.Atan(x / y) * (180 / Math.PI);

            if (radians < 0)
            {
                return radians + 360f;
            }

            return radians;
        }
    }
}