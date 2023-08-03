using System.Runtime.CompilerServices;
using Assets.Plugins.IvaLib.UnityLib.Factory;
using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Plugins.IvaLib.LeoEcsLite.EcsPools
{
    public abstract class EcsPoolBase<TGameObject, TFactory>
        where TGameObject : MonoBehaviour
        where TFactory : IFactory<TGameObject, TGameObject>
    {
        protected EcsPoolBase(
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
            return _factory.Create(_prefab, _position);
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