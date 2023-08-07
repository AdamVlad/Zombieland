using Assets.Game.Scripts.Levels.View;
using Assets.Game.Scripts.Levels.View.Widgets;

using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Infrastructure
{
    internal sealed class ViewsInstaller : MonoInstaller
    {
        [SerializeField] private PlayerHudView _playerHudView;

        public override void InstallBindings()
        {
            PlayerHudInstall();
        }

        private void PlayerHudInstall()
        {
            Container
                .Bind<PlayerHudView>()
                .FromInstance(_playerHudView)
                .AsSingle();
        }
    }
}