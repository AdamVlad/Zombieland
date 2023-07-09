using System.Collections.Generic;
using Assets.Game.Scripts.Levels.Model.Components.Weapons.Charges;
using Assets.Game.Scripts.Levels.Model.Pools;
using Assets.Game.Scripts.Levels.Model.Repositories;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsFactory;
using Leopotam.EcsLite;
using UnityEngine.Pool;

namespace Assets.Game.Scripts.Levels.Model.Services
{
    internal sealed class ChargesProviderService
    {
        public ChargesProviderService(
            IRepository<Charge> chargesRepository,
            IEcsFactory<Charge, Charge> chargesFactory,
            EcsWorld world)
        {
            _chargesRepository = chargesRepository;
            _chargesFactory = chargesFactory;
            _chargesPool = new Dictionary<ChargeType, IObjectPool<Charge>>();
            _world = world;
        }

        public void Run()
        {
            for (int i = 0; i < _chargesRepository.Count(); i++)
            {
                var charge = _chargesRepository.Get(i);
                var chargesPool = new ChargesPool(charge, 30, _chargesFactory, _world);

                if (_chargesPool.ContainsKey(charge.Type)) continue;

                _chargesPool.Add(charge.Type, chargesPool.Pool);
            }
        }

        public IObjectPool<Charge> GetPool(ChargeType chargesType)
        {
            return _chargesPool[chargesType];
        }

        private readonly IRepository<Charge> _chargesRepository;
        private readonly IEcsFactory<Charge, Charge> _chargesFactory;
        private readonly Dictionary<ChargeType, IObjectPool<Charge>> _chargesPool;
        private readonly EcsWorld _world;
    }
}