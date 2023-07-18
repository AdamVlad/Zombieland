using System.Collections.Generic;
using Assets.Game.Scripts.Levels.Model.Components.Charges;
using Assets.Game.Scripts.Levels.Model.Pools;
using Assets.Game.Scripts.Levels.Model.Repositories;
using Assets.Plugins.IvaLib.UnityLib.Factory;
using Leopotam.EcsLite;
using UnityEngine.Pool;

namespace Assets.Game.Scripts.Levels.Model.Services
{
    internal sealed class ChargesProviderService
    {
        public ChargesProviderService(
            IRepository<Charge> chargesRepository,
            IFactory<Charge, Charge> chargesFactory)
        {
            _chargesRepository = chargesRepository;
            _chargesFactory = chargesFactory;
            _chargesPool = new Dictionary<ChargeType, IObjectPool<Charge>>();
        }

        public void Run()
        {
            for (int i = 0; i < _chargesRepository.Count(); i++)
            {
                var charge = _chargesRepository.Get(i);
                var chargesPool = new ChargesPool(charge, 30, _chargesFactory);

                if (_chargesPool.ContainsKey(charge.Type)) continue;

                _chargesPool.Add(charge.Type, chargesPool.Pool);
            }
        }

        public IObjectPool<Charge> GetPool(ChargeType chargesType)
        {
            return _chargesPool[chargesType];
        }

        private readonly IRepository<Charge> _chargesRepository;
        private readonly IFactory<Charge, Charge> _chargesFactory;
        private readonly Dictionary<ChargeType, IObjectPool<Charge>> _chargesPool;
    }
}