using Assets.Plugins.IvaLib.UnityLib.Factories;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Assets.Plugins.IvaLib.UnityLib.Pools
{
    public abstract class UnityPoolBase<TGameObject, TFactory> : PoolBase<TGameObject, TFactory>
        where TGameObject : MonoBehaviour
        where TFactory : IUnityFactory<GameObject, TGameObject>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Construct(
            TFactory factory,
            GameObject prefab,
            Vector3 spawnPosition,
            Transform parent,
            int poolSize)
        {
            ConstructInternal(
                factory,
                prefab,
                spawnPosition,
                parent,
                poolSize);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override TGameObject CreatedPooledItem()
        {
            return _factory.Create(_gameObjectPrefab, ref _spawnPosition, _parent);
        }
    }
}