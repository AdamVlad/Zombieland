using Assets.Game.Scripts.Levels.Model.Components.Weapons;
using Assets.Game.Scripts.Levels.Model.Components.Weapons.Charges;
using UnityEngine;

using Zenject;
using Assets.Game.Scripts.Levels.Model.Factories;
using Assets.Game.Scripts.Levels.Model.Pools;
using UnityEngine.Pool;

namespace Assets.Game.Scripts.Levels.Model.Infrastructure
{
    public sealed class FactoriesInstaller : MonoInstaller
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _bulletsInitialParent;

        public override void InstallBindings()
        {
            var bulletsFactory = new BulletFactory();
            var bulletsPool = new BulletsPool(_bulletPrefab, 20, bulletsFactory, _bulletsInitialParent);

            Container
                .Bind<IObjectPool<Bullet>>()
                .FromInstance(bulletsPool.Pool)
                .AsSingle();
        }
    }
}
