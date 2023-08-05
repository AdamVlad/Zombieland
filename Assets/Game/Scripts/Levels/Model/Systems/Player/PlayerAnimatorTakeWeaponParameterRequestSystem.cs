using Assets.Game.Scripts.Levels.Model.Components.Data;
using Assets.Game.Scripts.Levels.Model.Components.Data.Player;
using Assets.Game.Scripts.Levels.Model.Components.Data.Requests;
using Assets.Game.Scripts.Levels.Model.Practices.Extensions;
using Assets.Game.Scripts.Levels.Model.ScriptableObjects;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;

using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Systems.Player
{
    internal sealed class PlayerAnimatorTakeWeaponParameterRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject
            <Inc<PlayerTagComponent,
                BackpackComponent>> _filter = default;

        private readonly EcsPoolInject<SetAnimatorParameterRequests> _animatorRequestPool = default;

        [Inject] private PlayerConfigurationSo _playerSettings;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var isWeaponInHand = _filter.Get2(entity).IsWeaponInHand;

                if (_animatorRequestPool.Has(entity))
                {
                    ref var requests = ref _animatorRequestPool.Get(entity);
                    requests.Add(_playerSettings.IsWeaponInHandParameter, isWeaponInHand);
                }
                else
                {
                    ref var requests = ref _animatorRequestPool.Add(entity);
                    requests.Initialize();
                    requests.Add(_playerSettings.IsWeaponInHandParameter, isWeaponInHand);
                }
            }
        }
    }
}