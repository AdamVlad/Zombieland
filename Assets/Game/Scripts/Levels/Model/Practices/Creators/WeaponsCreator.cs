using Assets.Game.Scripts.Levels.Model.Components.Data.Weapons;
using Assets.Game.Scripts.Levels.Model.Practices.Repositories;
using Assets.Plugins.IvaLib.UnityLib.Factory;

using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Practices.Creators
{
    internal class WeaponsCreator : ICreator<Weapon>
    {
        public WeaponsCreator(
            IRepository<Weapon> weaponsRepository,
            IFactory<Weapon, Weapon> weaponFactory)
        {
            _weaponsRepository = weaponsRepository;
            _weaponFactory = weaponFactory;
        }

        public Weapon CreateNext()
        {
            if (_nextIndex >= _weaponsRepository.Count()) return null;

            return _weaponFactory.Create(
                _weaponsRepository.Get(_nextIndex++),
                Vector3.zero);
        }

        public bool CanCreate() => _nextIndex < _weaponsRepository.Count();

        private readonly IRepository<Weapon> _weaponsRepository;
        private readonly IFactory<Weapon, Weapon> _weaponFactory;
        private int _nextIndex;
    }
}