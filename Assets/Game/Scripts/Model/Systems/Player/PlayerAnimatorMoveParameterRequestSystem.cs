using System.Runtime.CompilerServices;
using Assets.Game.Scripts.Model.Components;
using Assets.Game.Scripts.Model.Components.Requests;
using Assets.Game.Scripts.Model.Extensions;
using Assets.Plugins.IvaLib;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Game.Scripts.Model.Systems.Player
{
    internal sealed class PlayerAnimatorMoveParameterRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<
            PlayerTagComponent,
            MoveComponent,
            ShootingComponent>> _filter = default;

        private readonly EcsPoolInject<SetAnimatorParameterRequests> _animatorRequestPool = default;

        public void Run(IEcsSystems systems)
        {
            var pools = _filter.Pools;

            foreach (var entity in _filter.Value)
            {
                ref var moveComponent = ref pools.Inc2.Get(entity);
                ref var shootingComponent = ref pools.Inc3.Get(entity);

                if (!moveComponent.IsMoving)
                {
                    SetRequests(entity, 0, 0);
                    return;
                }

                Vector2 convertedInputAxis;

                if (!shootingComponent.IsShooting)
                {
                    convertedInputAxis = IvaMaths.ConvertCoordinatesToForward(ref moveComponent.MoveInputAxis);
                }
                else
                {
                    var origin = Vector3.zero;
                    var angle = IvaMaths.GetAngleBetween(
                        ref origin,
                        ref shootingComponent.Direction);

                    convertedInputAxis =
                        IvaMaths.ConvertCoordinatesRelativeShiftedSystemBy(
                            angle,
                            ref moveComponent.MoveInputAxis);
                }

                SetRequests(entity, convertedInputAxis.x, convertedInputAxis.y);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetRequests(int entity, float x, float y)
        {
            if (_animatorRequestPool.Value.Has(entity))
            {
                ref var requests = ref _animatorRequestPool.Value.Get(entity);
                AddRequest(ref requests, x, y);
            }
            else
            {
                ref var requests = ref _animatorRequestPool.Value.Add(entity);
                requests.Initialize();
                AddRequest(ref requests, x, y);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AddRequest(ref SetAnimatorParameterRequests requests, float x, float y)
        {
            requests
                .Add("x", x)    // задать в настройках
                .Add("y", y);
        }
    }
}