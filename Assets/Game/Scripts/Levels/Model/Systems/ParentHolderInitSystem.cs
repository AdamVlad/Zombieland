using Assets.Game.Scripts.Levels.Model.Components.Data;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;

using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Systems
{
    internal sealed class ParentHolderInitSystem : IEcsInitSystem
    {
        private readonly EcsFilterInject<Inc<ParentComponent>> _filter = default;

        private readonly EcsPoolInject<MonoLink<Transform>> _transformPool = default;

        public void Init(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                if (!_transformPool.Has(entity)) continue;

                ref var transformComponent = ref _transformPool.Get(entity);
                ref var parentComponent = ref _filter.Get1(entity);

                parentComponent.InitParentTransform = transformComponent.Value.parent;
                parentComponent.CurrentParentTransform = transformComponent.Value.parent;
            }
        }
    }
}