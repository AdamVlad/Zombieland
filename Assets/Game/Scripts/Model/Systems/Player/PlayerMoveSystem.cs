using Assets.Game.Scripts.Model.Components;
using Assets.Plugins.IvaLeoEcsLite.UnityEcsComponents;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Assets.Game.Scripts.Model.Systems.Player
{
    internal sealed class PlayerMoveSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                PlayerTagComponent,
                MonoLink<Transform>,
                MoveComponent,
                ShootingComponent,
                BackpackComponent>> _filter = default;

        public void Run(IEcsSystems systems)
        {
            var pools = _filter.Pools;

            foreach (var entity in _filter.Value)
            {
                ref var transform = ref pools.Inc2.Get(entity).Value;
                ref var moveComponent = ref pools.Inc3.Get(entity);
                ref var shootingComponent = ref pools.Inc4.Get(entity);
                ref var backpackComponent = ref pools.Inc5.Get(entity);

                if (!moveComponent.IsMoving) continue;

                if (shootingComponent.IsShooting && backpackComponent.IsWeaponInHand)
                {
                    MoveInAllDirections(ref transform, ref moveComponent);
                }
                else
                {
                    MoveOnlyForward(ref transform, ref moveComponent);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void MoveInAllDirections(ref Transform transform, ref MoveComponent moveComponent)
        {
            transform.position += new Vector3(
                moveComponent.MoveInputAxis.x * moveComponent.Speed,
                0, moveComponent.MoveInputAxis.y * moveComponent.Speed);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void MoveOnlyForward(ref Transform transform, ref MoveComponent moveComponent)
        {
            transform.position += transform.forward * moveComponent.Speed;
        }
    }
}