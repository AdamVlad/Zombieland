using Leopotam.EcsLite;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Infrastructure
{
    internal sealed class EcsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            DiContainerInstall();
        }

        private void DiContainerInstall()
        {
            EcsWorldInstall();
        }

        private void EcsWorldInstall()
        {
            Container
                .Bind<EcsWorld>()
                .AsSingle()
                .NonLazy();
        }
    }
}