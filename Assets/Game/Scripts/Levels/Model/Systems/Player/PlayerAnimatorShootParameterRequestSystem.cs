using Assets.Game.Scripts.Levels.Model.Components;
using Assets.Game.Scripts.Levels.Model.Components.Requests;
using Assets.Game.Scripts.Levels.Model.Extensions;
using Assets.Game.Scripts.Levels.Model.ScriptableObjects;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Game.Scripts.Levels.Model.Systems.Player
{
    internal sealed class PlayerAnimatorShootParameterRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<PlayerTagComponent,
                ShootingComponent>> _filter = default;

        private readonly EcsPoolInject<SetAnimatorParameterRequests> _animatorRequestPool = default;
        private readonly EcsCustomInject<PlayerConfigurationSo> _playerSettings = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var isShooting = _filter.Get2(entity).IsShooting;

                if (_animatorRequestPool.Has(entity))
                {
                    ref var requests = ref _animatorRequestPool.Get(entity);

                    requests.Add(_playerSettings.Value.IsShootingParameter, isShooting);
                    if (!isShooting) continue;
                    requests.Add(_playerSettings.Value.ShootParameter);
                }
                else
                {
                    ref var requests = ref _animatorRequestPool.Add(entity);
                    requests.Initialize();

                    requests.Add(_playerSettings.Value.IsShootingParameter, isShooting);
                    if (!isShooting) continue;
                    requests.Add(_playerSettings.Value.ShootParameter);
                }
            }
        }
    }
}