using System.Runtime.CompilerServices;

using Assets.Game.Scripts.Levels.Model.Components.Data;
using Assets.Game.Scripts.Levels.Model.Components.Data.Player;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;

using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Systems.Player
{
    internal sealed class PlayerMoveSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject
            <Inc<PlayerTagComponent,
                MonoLink<Transform>,
                PlayerMoveComponent,
                ShootingComponent,
                BackpackComponent>> _filter = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var transform = ref _filter.Get2(entity).Value;
                ref var moveComponent = ref _filter.Get3(entity);
                ref var shootingComponent = ref _filter.Get4(entity);
                ref var backpackComponent = ref _filter.Get5(entity);

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
        private void MoveInAllDirections(ref Transform transform, ref PlayerMoveComponent playerMoveComponent)
        {
            transform.position += new Vector3(
                playerMoveComponent.MoveInputAxis.x * playerMoveComponent.Speed,
                0, playerMoveComponent.MoveInputAxis.y * playerMoveComponent.Speed);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void MoveOnlyForward(ref Transform transform, ref PlayerMoveComponent playerMoveComponent)
        {
            transform.position += transform.forward * playerMoveComponent.Speed;
        }
    }
}