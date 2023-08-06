using Assets.Game.Scripts.Levels.Model.Practices.Builders.Context;
using Leopotam.EcsLite;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Infrastructure
{
    internal sealed class EcsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            EcsWorldInstall();
            EcsContextInstall();
        }

        private void EcsWorldInstall()
        {
            Container
                .Bind<EcsWorld>()
                .AsSingle()
                .NonLazy();
        }

        private void EcsContextInstall()
        {
            Container
                .Bind<EcsContext>()
                .AsSingle();
        }
    }
}