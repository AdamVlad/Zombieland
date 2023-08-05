using Assets.Game.Scripts.Levels.Model.Components.Data;
using Assets.Game.Scripts.Levels.Model.Components.Data.Enemies;
using Assets.Game.Scripts.Levels.Model.Components.Data.Processes;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsProcess;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Systems.Enemies
{
    internal sealed class EnemyHpBarLookAtSystem : IEcsRunSystem
    {
        [Inject] private readonly Camera _camera;

        private readonly EcsFilterInject
            <Inc<EnemyTagComponent,
                HpBarComponent,
                MonoLink<Transform>,
                Executing<HpBarActiveProcess>>> _filter = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var hpBarComponent = ref _filter.Get2(entity);
                ref var transformComponent = ref _filter.Get3(entity).Value;
                hpBarComponent.HpBarCanvas.transform.LookAt(transformComponent.position + _camera.transform.forward);
            }
        }
    }
}