﻿using Assets.Game.Scripts.Model.Components;
using Assets.Game.Scripts.Model.Components.Requests;
using Assets.Game.Scripts.Model.Extensions;
using Assets.Game.Scripts.Model.ScriptableObjects;
using Assets.Plugins.IvaLib.LeoEcsLite.Extensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Game.Scripts.Model.Systems.Player
{
    internal sealed class PlayerAnimatorTakeWeaponParameterRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<PlayerTagComponent,
                BackpackComponent>> _filter = default;

        private readonly EcsPoolInject<SetAnimatorParameterRequests> _animatorRequestPool = default;
        private readonly EcsCustomInject<BobConfigurationSO> _playerSettings = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var isWeaponInHand = _filter.Get2(entity).IsWeaponInHand;

                if (_animatorRequestPool.Has(entity))
                {
                    ref var requests = ref _animatorRequestPool.Get(entity);
                    requests.Add(_playerSettings.Value.IsWeaponInHandParameter, isWeaponInHand);
                }
                else
                {
                    ref var requests = ref _animatorRequestPool.Add(entity);
                    requests.Initialize();
                    requests.Add(_playerSettings.Value.IsWeaponInHandParameter, isWeaponInHand);
                }
            }
        }
    }
}