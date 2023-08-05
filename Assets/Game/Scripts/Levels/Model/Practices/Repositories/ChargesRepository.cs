using System;
using System.Collections.Generic;
using System.Linq;

using Assets.Game.Scripts.Levels.Model.Components.Data.Charges;

namespace Assets.Game.Scripts.Levels.Model.Practices.Repositories
{
    // Сделать абстрактный класс
    internal class ChargesRepository : IRepository<Charge>
    {
        public ChargesRepository(IEnumerable<Charge> objects)
        {
            _charges = objects.ToArray();
        }

        public Charge Get(int index)
        {
            if (index < 0 || index >= _charges.Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            return _charges[index];
        }

        public int Count() => _charges.Length;

        private readonly Charge[] _charges;
    }
}