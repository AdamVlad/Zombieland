using Assets.Plugins.IvaLib.LeoEcsLite.EcsFactory;
using Assets.Plugins.IvaLib.UnityLib.Pools;
using Leopotam.EcsLite;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Assets.Plugins.IvaLib.LeoEcsLite.EcsPool
{
    public abstract class EcsPoolBase<TGameObject, TFactory> : PoolBase<TGameObject, TFactory>
        where TGameObject : MonoBehaviour
        where TFactory : IEcsFactory<GameObject, TGameObject>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Construct(
            TFactory factory,
            GameObject prefab,
            Vector3 spawnPosition,
            Transform parent,
            EcsWorld world,
            int poolSize)
        {
            ConstructInternal(
                factory,
                prefab,
                spawnPosition,
                parent,
                poolSize);

            _world = world;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override TGameObject CreatedPooledItem()
        {
            return _factory.Create(_gameObjectPrefab, ref _spawnPosition, _parent, _world);
        }

        protected EcsWorld _world;
    }
}