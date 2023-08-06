using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Infrastructure
{
    internal sealed class SharedDataInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            EventsBusInstall();
            MainCameraInstall();
        }

        private void EventsBusInstall()
        {
            Container
                .Bind<EventsBus>()
                .FromInstance(new EventsBus(32))
                .AsSingle();
        }

        private void MainCameraInstall()
        {
            Container
                .Bind<Camera>()
                .FromInstance(Camera.main)
                .AsSingle();
        }
    }
}