using Assets.Game.Scripts.Levels.Model.Components.Data.Charges;
using Assets.Game.Scripts.Levels.Model.Components.Data.Weapons;
using Assets.Game.Scripts.Levels.Model.Practices.Builders;
using Assets.Game.Scripts.Levels.Model.Practices.Factories;

using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Infrastructure
{
    internal sealed class FactoriesInstaller : MonoInstaller
    {
        [Inject] private EcsWorld _world;

        [SerializeField] private Transform _chargesInitialParent;

        public override void InstallBindings()
        {
            EnemyFactoryInstall();
            PlayerFactoryInstall();

            Container
                .Bind<Plugins.IvaLib.UnityLib.Factory.IFactory<Weapon, Weapon>>()
                .To<WeaponFactory>()
                .FromInstance(new WeaponFactory(_world))
                .AsSingle();
        }

        private void EnemyFactoryInstall()
        {
            Container
                .Bind<EnemyFactory>()
                .AsSingle();
        }

        private void PlayerFactoryInstall()
        {
            Container
                .Bind<PlayerBuilder>()
                .AsSingle();

            Container
                .Bind<PlayerFactory>()
                .AsSingle();
        }
    }
}