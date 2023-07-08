﻿using UnityEngine;
using UnityEngine.Pool;
using Leopotam.EcsLite;

using System.Runtime.CompilerServices;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsFactory;


namespace Assets.Plugins.IvaLib.LeoEcsLite.Pools
{
    public abstract class EcsPoolBase<TGameObject, TFactory>
        where TGameObject : MonoBehaviour
        where TFactory : IEcsFactory<TGameObject, TGameObject>
    {
        protected EcsPoolBase(
            TGameObject prefab,
            int poolSize,
            TFactory factory,
            EcsWorld world)
        {
            _prefab = prefab;
            _poolSize = poolSize;
            _factory = factory;
            _world = world;
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
        private EcsWorld _world;

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
            return _factory.Create(_prefab, _position, _world);
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