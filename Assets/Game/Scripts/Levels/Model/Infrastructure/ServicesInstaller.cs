using Assets.Game.Scripts.Levels.Model.Components.Charges;
using Assets.Game.Scripts.Levels.Model.Creators;
using Assets.Game.Scripts.Levels.Model.Factories;
using Assets.Game.Scripts.Levels.Model.Repositories;
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
            var weaponsRepository = new WeaponsRepository(_weapons);
            var weaponFactory = new WeaponFactory(_world, _weaponsInitialParent);
            var weaponsCreator = new WeaponsCreator(
                weaponsRepository,
                weaponFactory);

            Container
                .Bind<WeaponsProviderService>()
                .FromInstance(new WeaponsProviderService(weaponsCreator))
                .AsSingle();
        }

        private void ChargesProviderServiceInstall()
        {
            var chargesRepository = new ChargesRepository(_charges);
            var chargesFactory = new ChargesFactory(_world, _chargesInitialParent);

            Container
                .Bind<ChargesProviderService>()
                .FromInstance(new ChargesProviderService(chargesRepository, chargesFactory))
                .AsSingle();
        }
    }
}