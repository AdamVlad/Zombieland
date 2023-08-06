using Assets.Game.Scripts.Levels.Model.Components.Data;
using Assets.Game.Scripts.Levels.Model.Components.Data.Player;
using Assets.Game.Scripts.Levels.Model.Components.Data.Requests;
using Assets.Game.Scripts.Levels.Model.Practices.Extensions;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;

using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Game.Scripts.Levels.Model.Systems.Player
{
    internal sealed class PlayerAnimatorTakeWeaponParameterRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject
            <Inc<PlayerTagComponent,
                BackpackComponent,
            MonoLink<Components.Data.Player.Player>>> _filter = default;

        private readonly EcsPoolInject<SetAnimatorParameterRequests> _animatorRequestPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var isWeaponInHand = _filter.Get2(entity).IsWeaponInHand;
                var playerSettings = _filter.Get3(entity).Value.Settings;

                if (_animatorRequestPool.Has(entity))
                {
                    ref var requests = ref _animatorRequestPool.Get(entity);
                    requests.Add(playerSettings.IsWeaponInHandParameter, isWeaponInHand);
                }
                else
                {
                    ref var requests = ref _animatorRequestPool.Add(entity);
                    requests.Initialize();
                    requests.Add(playerSettings.IsWeaponInHandParameter, isWeaponInHand);
                }
            }
        }
    }
}