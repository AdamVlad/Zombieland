using System.Collections.Generic;
using Assets.Game.Scripts.Levels.Model.Components.Weapons.Charges;
using Assets.Game.Scripts.Levels.Model.Pools;
using Assets.Game.Scripts.Levels.Model.Repositories;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsFactory;
using Leopotam.EcsLite;
using UnityEngine.Pool;

namespace Assets.Game.Scripts.Levels.Model.Services
{
    internal sealed class BulletsProviderService
    {
        public BulletsProviderService(
            IRepository<Bullet> bulletsRepository,
            IEcsFactory<Bullet, Bullet> bulletsFactory,
            EcsWorld world)
        {
            _bulletsRepository = bulletsRepository;
            _bulletsFactory = bulletsFactory;
            _bulletsPool = new Dictionary<ChargeType, IObjectPool<Bullet>>();
            _world = world;
        }

        public void Run()
        {
            for (int i = 0; i < _bulletsRepository.Count(); i++)
            {
                var bullet = _bulletsRepository.Get(i);
                var bulletsPool = new BulletsPool(bullet, 30, _bulletsFactory, _world);

                if (_bulletsPool.ContainsKey(bullet.Type)) continue;

                _bulletsPool.Add(bullet.Type, bulletsPool.Pool);
            }
        }

        public IObjectPool<Bullet> GetPool(ChargeType bulletsType)
        {
            return _bulletsPool[bulletsType];
        }

        private readonly IRepository<Bullet> _bulletsRepository;
        private readonly IEcsFactory<Bullet, Bullet> _bulletsFactory;
        private readonly Dictionary<ChargeType, IObjectPool<Bullet>> _bulletsPool;
        private readonly EcsWorld _world;
    }
}