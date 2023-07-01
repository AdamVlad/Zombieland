using Assets.Plugins.IvaLib.UnityLib.Factories;
using UnityEngine.Pool;
using UnityEngine;
using System.Runtime.CompilerServices;

namespace Assets.Plugins.IvaLib.UnityLib.Pools
{
    public abstract class PoolBase<TGameObject, TFactory> : MonoBehaviour
    where TGameObject : MonoBehaviour
    where TFactory : IFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void ConstructInternal(
            TFactory factory,
            GameObject prefab,
            Vector3 spawnPosition,
            Transform parent,
            int poolSize)
        {
            _factory = factory;
            _gameObjectPrefab = prefab;
            _spawnPosition = spawnPosition;
            _parent = parent;
            _poolSize = poolSize;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void ProvideSpawnPoint(Vector3 spawnPosition)
        {
            _spawnPosition = spawnPosition;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void ProvidePrefab(GameObject prefab)
        {
            _gameObjectPrefab = prefab;
        }

        public IObjectPool<TGameObject> Pool =>
            _pool ??= new ObjectPool<TGameObject>(
                CreatedPooledItem,
                OnTakeFromPool,
                OnReturnedToPool,
                OnDestroyPoolObject,
                true,
                _poolSize,
                _poolSize);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract TGameObject CreatedPooledItem();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void OnTakeFromPool(TGameObject createdObject)
        {
            createdObject.gameObject.SetActive(true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void OnReturnedToPool(TGameObject createdObject)
        {
            createdObject.gameObject.SetActive(false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void OnDestroyPoolObject(TGameObject createdObject)
        {
            Destroy(createdObject.gameObject);
        }

        protected IObjectPool<TGameObject> _pool;
        protected TFactory _factory;
        protected GameObject _gameObjectPrefab;
        protected Vector3 _spawnPosition;
        protected Transform _parent;
        protected int _poolSize;
    }
}