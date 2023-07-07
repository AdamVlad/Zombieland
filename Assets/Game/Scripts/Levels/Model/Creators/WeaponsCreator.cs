using Assets.Game.Scripts.Levels.Model.Repositories;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsFactory;
using Leopotam.EcsLite;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Creators
{
    internal class WeaponsCreator : ICreator<GameObject>
    {
        public WeaponsCreator(
            IRepository<GameObject> weaponsRepository,
            IEcsFactory<GameObject, GameObject> weaponFactory,
            Transform parent,
            EcsWorld world)
        {
            _weaponsRepository = weaponsRepository;
            _weaponFactory = weaponFactory;
            _parent = parent;
            _world = world;
        }

        public GameObject CreateNext()
        {
            if (_nextIndex >= _weaponsRepository.Count()) return null;

            return _weaponFactory.Create(
                _weaponsRepository.Get(_nextIndex++),
                Vector3.zero,
                _parent,
                _world);
        }

        public bool CanCreate() => _nextIndex < _weaponsRepository.Count();

        private readonly IRepository<GameObject> _weaponsRepository;
        private readonly IEcsFactory<GameObject, GameObject> _weaponFactory;
        private readonly Transform _parent;
        private readonly EcsWorld _world;
        private int _nextIndex;
    }
}