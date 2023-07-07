using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Game.Scripts.Levels.Model.Components.Weapons;
using Assets.Game.Scripts.Levels.Model.Components.Weapons.Charges;

namespace Assets.Game.Scripts.Levels.Model.Repositories
{
    internal class BulletsRepository : IRepository<Bullet>
    {
        public BulletsRepository(IEnumerable<Bullet> objects)
        {
            _bullets = objects.ToArray();
        }

        public Bullet Get(int index)
        {
            if (index < 0 || index >= _bullets.Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            return _bullets[index];
        }

        public int Count() => _bullets.Length;

        private readonly Bullet[] _bullets;
    }
}