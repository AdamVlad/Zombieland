using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Repositories
{
    internal class WeaponsRepository : IRepository<GameObject>
    {
        public WeaponsRepository(IEnumerable<GameObject> objects)
        {
            _weapons = objects.ToArray();
        }

        public GameObject Get(int index)
        {
            if (index < 0 || index >= _weapons.Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            return _weapons[index];
        }

        public int Count() => _weapons.Length;

        private readonly GameObject[] _weapons;
    }
}