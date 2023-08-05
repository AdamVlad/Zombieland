using Assets.Game.Scripts.Levels.Model.Practices.Repositories;
using Assets.Plugins.IvaLib.UnityLib.Factory;

using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Practices.Creators
{
    internal class WeaponsCreator : ICreator<GameObject>
    {
        public WeaponsCreator(
            IRepository<GameObject> weaponsRepository,
            IFactory<GameObject, GameObject> weaponFactory)
        {
            _weaponsRepository = weaponsRepository;
            _weaponFactory = weaponFactory;
        }

        public GameObject CreateNext()
        {
            if (_nextIndex >= _weaponsRepository.Count()) return null;

            return _weaponFactory.Create(
                _weaponsRepository.Get(_nextIndex++),
                Vector3.zero);
        }

        public bool CanCreate() => _nextIndex < _weaponsRepository.Count();

        private readonly IRepository<GameObject> _weaponsRepository;
        private readonly IFactory<GameObject, GameObject> _weaponFactory;
        private int _nextIndex;
    }
}