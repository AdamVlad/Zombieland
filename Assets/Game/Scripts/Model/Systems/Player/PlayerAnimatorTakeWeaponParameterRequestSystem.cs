using Assets.Game.Scripts.Model.Components;
using Assets.Game.Scripts.Model.Components.Requests;
using Assets.Game.Scripts.Model.Extensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Game.Scripts.Model.Systems.Player
{
    internal sealed class PlayerAnimatorTakeWeaponParameterRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<
            PlayerTagComponent,
            BackpackComponent>> _filter = default;

        private readonly EcsPoolInject<SetAnimatorParameterRequests> _animatorRequestPool = default;

        public void Run(IEcsSystems systems)
        {
            var pools = _filter.Pools;

            foreach (var entity in _filter.Value)
            {
                var hasWeapon = pools.Inc2.Get(entity).WeaponEntity != -1;

                if (_animatorRequestPool.Value.Has(entity))
                {
                    ref var requests = ref _animatorRequestPool.Value.Get(entity);
                    requests.Add("WeaponInHand", hasWeapon);
                }
                else
                {
                    ref var requests = ref _animatorRequestPool.Value.Add(entity);
                    requests.Initialize();
                    requests.Add("WeaponInHand", hasWeapon);
                }
            }
        }
    }
}