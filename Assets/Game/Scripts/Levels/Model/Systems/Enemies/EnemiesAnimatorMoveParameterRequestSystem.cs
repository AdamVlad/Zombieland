using System.Runtime.CompilerServices;
using Assets.Game.Scripts.Levels.Model.Components.Enemies;
using Assets.Game.Scripts.Levels.Model.Components.Requests;
using Assets.Game.Scripts.Levels.Model.Extensions;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine.AI;

namespace Assets.Game.Scripts.Levels.Model.Systems.Enemies
{
    internal sealed class EnemiesAnimatorMoveParameterRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject
        <Inc<EnemyTagComponent,
            MonoLink<NavMeshAgent>,
            MonoLink<Enemy>>> _filter = default;

        private readonly EcsPoolInject<SetAnimatorParameterRequests> _animatorRequestPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var navMeshAgentComponent = ref _filter.Get2(entity).Value;
                ref var enemyComponent = ref _filter.Get3(entity).Value;

                SetRequests(
                    entity, 
                    navMeshAgentComponent.velocity.x, 
                    navMeshAgentComponent.velocity.z,
                    enemyComponent.Settings.MoveXParameter,
                    enemyComponent.Settings.MoveYParameter);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetRequests(int entity, float x, float y, string parameterX, string parameterY)
        {
            if (_animatorRequestPool.Has(entity))
            {
                ref var requests = ref _animatorRequestPool.Get(entity);
                AddRequest(ref requests, x, y, parameterX, parameterY);
            }
            else
            {
                ref var requests = ref _animatorRequestPool.Add(entity);
                requests.Initialize();
                AddRequest(ref requests, x, y, parameterX, parameterY);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AddRequest(ref SetAnimatorParameterRequests requests, float x, float y, string parameterX, string parameterY)
        {
            requests
                .Add(parameterX, x)
                .Add(parameterY, y);
        }
    }
}