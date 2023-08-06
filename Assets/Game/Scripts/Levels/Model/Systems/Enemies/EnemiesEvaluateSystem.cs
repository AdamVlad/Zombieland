using Assets.Game.Scripts.Levels.Model.Components.Behaviours;
using Assets.Game.Scripts.Levels.Model.Components.Data;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;

using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Game.Scripts.Levels.Model.Systems.Enemies
{
    internal sealed class EnemiesEvaluateSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject
            <Inc<MonoLink<BehavioursScope>,
                BehaviourComponent>> _filter = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var behavioursScopeComponent = ref _filter.Get1(entity).Value;
                ref var behaviourComponent = ref _filter.Get2(entity);

                var highestEvaluate = float.MinValue;
                behaviourComponent.ActiveBehaviour = null;

                foreach (var behaviour in behavioursScopeComponent.Behaviours)
                {
                    var currentEvaluate = behaviour.Evaluate();

                    if (currentEvaluate <= highestEvaluate) continue;

                    highestEvaluate = currentEvaluate;
                    behaviourComponent.ActiveBehaviour = behaviour;
                }
            }
        }
    }
}