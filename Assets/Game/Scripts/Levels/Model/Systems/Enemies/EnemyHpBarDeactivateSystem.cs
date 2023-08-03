using Assets.Game.Scripts.Levels.Model.Components.Data;
using Assets.Game.Scripts.Levels.Model.Components.Data.Enemies;
using Assets.Game.Scripts.Levels.Model.Components.Data.Processes;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsProcess;

using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Game.Scripts.Levels.Model.Systems.Enemies
{
    internal sealed class EnemyHpBarDeactivateSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject
            <Inc<EnemyTagComponent,
                HpBarComponent,
                Completed<HpBarActiveProcess>>> _filter = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var hpBarComponent = ref _filter.Get2(entity);
                hpBarComponent.HpBarCanvas.enabled = false;
            }
        }
    }
}