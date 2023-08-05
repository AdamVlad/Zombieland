using Assets.Game.Scripts.Levels.Model.Components.Data.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Game.Scripts.Levels.Model.Practices.Repositories
{
    internal class WeaponsRepository : IRepository<Weapon>
    {
        public WeaponsRepository(IEnumerable<Weapon> objects)
        {
            _weapons = objects.ToArray();
        }

        public Weapon Get(int index)
        {
            if (index < 0 || index >= _weapons.Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            return _weapons[index];
        }

        public int Count() => _weapons.Length;

        private readonly Weapon[] _weapons;
    }
}