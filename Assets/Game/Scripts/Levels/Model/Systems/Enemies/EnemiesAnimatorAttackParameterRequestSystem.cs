using Assets.Game.Scripts.Levels.Model.Components.Data;
using Assets.Game.Scripts.Levels.Model.Components.Data.Enemies;
using Assets.Game.Scripts.Levels.Model.Components.Data.Requests;
using Assets.Game.Scripts.Levels.Model.Extensions;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Game.Scripts.Levels.Model.Systems.Enemies
{
    internal sealed class EnemiesAnimatorAttackParameterRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject
            <Inc<EnemyTagComponent,
                ShootingComponent,
                MonoLink<Enemy>>> _filter = default;

        private readonly EcsPoolInject<SetAnimatorParameterRequests> _animatorRequestPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var attackComponent = ref _filter.Get2(entity);

                var enemy = _filter.Get3(entity).Value;

                if (_animatorRequestPool.Has(entity))
                {
                    ref var requests = ref _animatorRequestPool.Get(entity);
                    requests.Add(enemy.Settings.AttackTriggerParameter, attackComponent.IsShooting);
                }
                else
                {
                    ref var requests = ref _animatorRequestPool.Add(entity);
                    requests.Initialize();
                    requests.Add(enemy.Settings.AttackTriggerParameter, attackComponent.IsShooting);
                }

                attackComponent.IsShooting = false;
            }
        }
    }
}