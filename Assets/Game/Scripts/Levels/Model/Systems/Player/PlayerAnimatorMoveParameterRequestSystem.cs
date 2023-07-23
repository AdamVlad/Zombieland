using System.Runtime.CompilerServices;
using Assets.Game.Scripts.Levels.Model.Components;
using Assets.Game.Scripts.Levels.Model.Components.Player;
using Assets.Game.Scripts.Levels.Model.Components.Requests;
using Assets.Game.Scripts.Levels.Model.Extensions;
using Assets.Game.Scripts.Levels.Model.ScriptableObjects;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.UnityLib;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Systems.Player
{
    internal sealed class PlayerAnimatorMoveParameterRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject
            <Inc<PlayerTagComponent,
                MoveComponent,
                ShootingComponent,
                BackpackComponent>> _filter = default;

        private readonly EcsPoolInject<SetAnimatorParameterRequests> _animatorRequestPool = default;

        [Inject] private PlayerConfigurationSo _playerSettings;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var moveComponent = ref _filter.Get2(entity);
                ref var shootingComponent = ref _filter.Get3(entity);
                ref var backpackComponent = ref _filter.Get4(entity);

                if (!moveComponent.IsMoving)
                {
                    SetRequests(entity, 0, 0);
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

                SetRequests(entity, convertedInputAxis.x, convertedInputAxis.y);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetRequests(int entity, float x, float y)
        {
            if (_animatorRequestPool.Has(entity))
            {
                ref var requests = ref _animatorRequestPool.Get(entity);
                AddRequest(ref requests, x, y);
            }
            else
            {
                ref var requests = ref _animatorRequestPool.Add(entity);
                requests.Initialize();
                AddRequest(ref requests, x, y);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AddRequest(ref SetAnimatorParameterRequests requests, float x, float y)
        {
            requests
                .Add(_playerSettings.MoveXParameter, x)
                .Add(_playerSettings.MoveYParameter, y);
        }
    }
}