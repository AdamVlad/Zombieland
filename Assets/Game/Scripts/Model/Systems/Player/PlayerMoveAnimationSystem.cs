using Assets.Game.Scripts.Model.Components;
using Assets.Plugins.IvaLeoEcsLite.UnityEcsComponents;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Game.Scripts.Model.Systems.Player
{
    internal sealed class PlayerMoveAnimationSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MonoLink<Animator>, InputMoveComponent, BackpackComponent>> _filter = default;//GlobalIdents.EventWorldName;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var pools = _filter.Pools;

                ref var animator = ref pools.Inc1.Get(entity).Value;
                ref var moveInput = ref pools.Inc2.Get(entity).MoveInput;
                ref var weaponEntity = ref pools.Inc3.Get(entity).WeaponEntity;

                //animator.SetFloat("x", moveInput.x);
                //animator.SetFloat("y", moveInput.y);

                animator.SetBool("WeaponInHand", weaponEntity != -1);
                animator.SetFloat("x", 0);
                animator.SetFloat("y", Mathf.Sqrt(moveInput.x * moveInput.x + moveInput.y * moveInput.y));
            }
        }
    }
}
