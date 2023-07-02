using Assets.Game.Scripts.Model.Repositories;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsFactory;
using Leopotam.EcsLite;
using UnityEngine;

namespace Assets.Game.Scripts.Model.Creators
{
    internal class WeaponsCreator : ICreator<GameObject>
    {
        public WeaponsCreator(
            IRepository<GameObject> repository,
            IEcsFactory<GameObject, GameObject> factory,
            Transform parent,
            EcsWorld world)
        {
            _repository = repository;
            _factory = factory;
            _parent = parent;
            _world = world;
        }

        public GameObject CreateNext()
        {
            if (_nextIndex >= _repository.Count()) return null;

            return _factory.Create(
                _repository.Get(_nextIndex++),
                Vector3.zero,
                _parent,
                _world);
        }

        public bool CanCreate() => _nextIndex < _repository.Count();

        private readonly IRepository<GameObject> _repository;
        private readonly IEcsFactory<GameObject, GameObject> _factory;
        private readonly Transform _parent;
        private readonly EcsWorld _world;
        private int _nextIndex;
    }
}