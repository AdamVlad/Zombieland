using Assets.Game.Scripts.Levels.Model.Practices.Builders;
using Assets.Game.Scripts.Levels.Model.Practices.Factories;

using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Infrastructure
{
    internal sealed class FactoriesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            EnemyFactoryInstall();
            PlayerFactoryInstall();
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