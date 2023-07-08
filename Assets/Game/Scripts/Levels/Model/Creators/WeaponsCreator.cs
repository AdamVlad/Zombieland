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
            EcsWorld world)
        {
            _weaponsRepository = weaponsRepository;
            _weaponFactory = weaponFactory;
            _world = world;
        }

        public GameObject CreateNext()
        {
            if (_nextIndex >= _weaponsRepository.Count()) return null;

            return _weaponFactory.Create(
                _weaponsRepository.Get(_nextIndex++),
                Vector3.zero,
                _world);
        }

        public bool CanCreate() => _nextIndex < _weaponsRepository.Count();

        private readonly IRepository<GameObject> _weaponsRepository;
        private readonly IEcsFactory<GameObject, GameObject> _weaponFactory;
        private readonly EcsWorld _world;
        private int _nextIndex;
    }
}