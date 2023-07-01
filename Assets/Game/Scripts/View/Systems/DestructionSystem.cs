using Assets.Game.Scripts.Model.Components.Delayed;
using Assets.Game.Scripts.Model.Components.Items;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Game.Scripts.View.Systems
{
    internal sealed class DestructionSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DestructionDelayed>> _filter = default;

        private readonly EcsPoolInject<DestructionDelayed> _destructionPool = default;
        private readonly EcsPoolInject<MonoLink<PoolingObject>> _poolingObjectPool = default;
        private readonly EcsPoolInject<MonoLink<Transform>> _transformPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                if (_poolingObjectPool.Has(entity))
                {
                    ref var poolingObject = ref _poolingObjectPool.Get(entity).Value;
                    poolingObject.ReturnToPool();
                }
                else if (_transformPool.Has(entity))
                {
                    ref var transform = ref _transformPool.Get(entity).Value;
                    transform.gameObject.SetActive(false);
                }

                _destructionPool.Del(entity);
            }
        }
    }
}