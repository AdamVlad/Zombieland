using Assets.Game.Scripts.Levels.Model.Components.Data;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Game.Scripts.Levels.Model.Systems.Enemies
{
    internal sealed class EnemiesBehaveSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<BehaviourComponent>> _filter = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var behaviourComponent = ref _filter.Get1(entity);
                behaviourComponent.ActiveBehaviour?.Behave();
            }
        }
    }
}