using UnityEngine;
using UnityEngine.Pool;

using System.Runtime.CompilerServices;
using Assets.Plugins.IvaLib.UnityLib.Factory;

namespace Assets.Plugins.IvaLib.UnityLib.Pools
{
    public abstract class PoolsItemBase<TObject> : MonoBehaviour
        where TObject : MonoBehaviour
    {
        public IObjectPool<TObject> Pool { get; set; }
    }

    public abstract class PoolBase<TGameObject, TFactory>
        where TFactory : IFactory<TGameObject, TGameObject>
        where TGameObject : PoolsItemBase<TGameObject>
    {
        protected PoolBase(
            TGameObject prefab,
            int poolSize,
            TFactory factory)
        {
            _prefab = prefab;
            _poolSize = poolSize;
            _factory = factory;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ProvideSpawnPosition(Vector3 position)
        {
            _position = position;
        }

        private IObjectPool<TGameObject> _pool;
        private TFactory _factory;
        private TGameObject _prefab;
        private Vector3 _position;
        private int _poolSize;

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
        private TGameObject CreatedPooledItem()
        {
            var item = _factory.Create(_prefab, _position);
            item.Pool = Pool;
            return item;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void OnTakeFromPool(TGameObject createdObject)
        {
            createdObject.gameObject.SetActive(true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void OnReturnedToPool(TGameObject createdObject)
        {
            createdObject.gameObject.SetActive(false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void OnDestroyPoolObject(TGameObject createdObject)
        {
            Object.Destroy(createdObject);
        }
    }
}