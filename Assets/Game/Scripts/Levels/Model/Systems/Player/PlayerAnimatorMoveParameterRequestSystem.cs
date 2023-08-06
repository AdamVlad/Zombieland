using System.Runtime.CompilerServices;

using Assets.Game.Scripts.Levels.Model.Components.Data;
using Assets.Game.Scripts.Levels.Model.Components.Data.Player;
using Assets.Game.Scripts.Levels.Model.Components.Data.Requests;
using Assets.Game.Scripts.Levels.Model.Practices.Extensions;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;
using Assets.Plugins.IvaLib.UnityLib;

using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Systems.Player
{
    internal sealed class PlayerAnimatorMoveParameterRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject
            <Inc<PlayerTagComponent,
                PlayerMoveComponent,
                ShootingComponent,
                BackpackComponent>> _filter = default;

        private readonly EcsPoolInject<SetAnimatorParameterRequests> _animatorRequestPool = default;
        private readonly EcsPoolInject<MonoLink<Components.Data.Player.Player>> _playerPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var moveComponent = ref _filter.Get2(entity);
                ref var shootingComponent = ref _filter.Get3(entity);
                ref var backpackComponent = ref _filter.Get4(entity);
                ref var player = ref _playerPool.Get(entity).Value;

                if (!moveComponent.IsMoving)
                {
                    SetRequests(
                        entity,
                        player.Settings.MoveXParameter,
                        player.Settings.MoveYParameter,
                        0,
                        0);
                    return;
                }

                Vector2 convertedInputAxis;

                if (!shootingComponent.IsShooting || !backpackComponent.IsWeaponInHand)
                {
                    convertedInputAxis = IvaMaths.ConvertCoordinatesToForward(ref moveComponent.MoveInputAxis);
                }
                else
                {
                    var origin = Vector3.zero;
                    var angle = IvaMaths.GetAngle360Between(
                        ref origin,
                        ref shootingComponent.Direction);

                    convertedInputAxis =
                        IvaMaths.ConvertCoordinatesRelativeShiftedSystemBy(
                            angle,
                            ref moveComponent.MoveInputAxis);
                }

                SetRequests(
                    entity, 
                    player.Settings.MoveXParameter,
                    player.Settings.MoveYParameter,
                    convertedInputAxis.x,
                    convertedInputAxis.y);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetRequests(int entity, string xParameter, string yParameter, float x, float y)
        {
            if (_animatorRequestPool.Has(entity))
            {
                ref var requests = ref _animatorRequestPool.Get(entity);
                AddRequest(ref requests, xParameter, yParameter, x, y);
            }
            else
            {
                ref var requests = ref _animatorRequestPool.Add(entity);
                requests.Initialize();
                AddRequest(ref requests, xParameter, yParameter, x, y);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AddRequest(
            ref SetAnimatorParameterRequests requests,
            string xParameter,
            string yParameter,
            float x,
            float y)
        {
            requests
                .Add(xParameter, x)
                .Add(yParameter, y);
        }
    }
}