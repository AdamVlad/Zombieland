using Assets.Game.Scripts.Levels.Model.Components.Data.Charges;
using Assets.Game.Scripts.Levels.Model.Components.Data.Weapons;
using Assets.Game.Scripts.Levels.Model.Practices.Creators;
using Assets.Game.Scripts.Levels.Model.Practices.Factories;
using Assets.Game.Scripts.Levels.Model.Practices.Repositories;
using Assets.Game.Scripts.Levels.Model.Services;

using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Infrastructure
{
    internal sealed class ServicesInstaller : MonoInstaller
    {
        [Inject] private EcsWorld _world;

        [Space, Header("Weapons")]
        [SerializeField] private GameObject[] _weapons;
        [SerializeField] private Transform _weaponsInitialParent;

        [Space, Header("Charges")]
        [SerializeField] private Charge[] _charges;
        [SerializeField] private Transform _chargesInitialParent;

        public override void InstallBindings()
        {
            WeaponsProviderServiceInstall();
            ChargesProviderServiceInstall();
        }

        private void WeaponsProviderServiceInstall()
        {
            var temp = new Weapon[_weapons.Length];
            for (int i = 0; i < _weapons.Length; i++)
            {
                temp[i] = _weapons[i].GetComponent<Weapon>();
            }

            Container
                .Bind<IRepository<Weapon>>()
                .To<WeaponsRepository>()
                .FromInstance(new WeaponsRepository(temp))
                .AsSingle();

            Container
                .Bind<Plugins.IvaLib.UnityLib.Factory.IFactory<Weapon, Weapon>>()
                .To<WeaponFactory>()
                .FromInstance(new WeaponFactory(_world, _weaponsInitialParent))
                .AsSingle();

            Container
                .Bind<ICreator<Weapon>>()
                .To<WeaponsCreator>()
                .AsSingle();

            Container
                .Bind<WeaponsProviderService>()
                .AsSingle();
        }

        private void ChargesProviderServiceInstall()
        {
            Container
                .Bind<IRepository<Charge>>()
                .To<ChargesRepository>()
                .FromInstance(new ChargesRepository(_charges))
                .AsSingle();

            Container
                .Bind<Plugins.IvaLib.UnityLib.Factory.IFactory<Charge, Charge>>()
                .To<ChargesFactory>()
                .FromInstance(new ChargesFactory(_world, _chargesInitialParent))
                .AsSingle();

            Container
                .Bind<ChargesProviderService>()
                .ToSelf()
                .AsSingle();
        }
    }
}