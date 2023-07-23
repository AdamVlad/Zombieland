using Assets.Game.Scripts.Levels.Model.Factories;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Infrastructure
{
    internal sealed class EnemyFactoryInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            EnemyFactoryInstall();
        }

        private void EnemyFactoryInstall()
        {
            Container
                .Bind<EnemyFactory>()
                .AsSingle();
        }
    }
}