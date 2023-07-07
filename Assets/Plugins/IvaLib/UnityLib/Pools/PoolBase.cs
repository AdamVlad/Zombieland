using UnityEngine;
using UnityEngine.Pool;

using Assets.Plugins.IvaLib.UnityLib.Factory;

namespace Assets.Plugins.IvaLib.UnityLib.Pools
{
    public abstract class PoolBase<TGameObject, TFactory>
        where TGameObject : MonoBehaviour
        where TFactory : IObjectFactory<TGameObject, TGameObject>
    {
        protected PoolBase(
            TGameObject prefab,
            int poolSize,
            TFactory factory,
            Transform parent)
        {
            _prefab = prefab;
            _poolSize = poolSize;
            _factory = factory;
            _parent = parent;
        }

        public void ProvideSpawnPosition(Vector3 position)
        {
            _position = position;
        }

        private IObjectPool<TGameObject> _pool;
        private TFactory _factory;
        private TGameObject _prefab;
        private Vector3 _position;
        private int _poolSize;
        private Transform _parent;

        public IObjectPool<TGameObject> Pool =>
            _pool ??= new ObjectPool<TGameObject>(
                CreatedPooledItem,
                OnTakeFromPool,
                OnReturnedToPool,
                OnDestroyPoolObject,
                true,
                _poolSize,
                _poolSize);

        private TGameObject CreatedPooledItem()
        {
            return _factory.Create(_prefab, _parent, _position);
        }

        private void OnTakeFromPool(TGameObject createdObject)
        {
            createdObject.gameObject.SetActive(true);
        }

        private void OnReturnedToPool(TGameObject createdObject)
        {
            createdObject.gameObject.SetActive(false);
        }

        private void OnDestroyPoolObject(TGameObject createdObject)
        {
            Object.Destroy(createdObject);
        }
    }
}