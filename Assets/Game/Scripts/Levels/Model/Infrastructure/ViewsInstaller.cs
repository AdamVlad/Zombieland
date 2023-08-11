using Assets.Game.Scripts.Levels.View;

using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Infrastructure
{
    internal sealed class ViewsInstaller : MonoInstaller
    {
        [SerializeField] private PlayerHudView _playerHudView;
        [SerializeField] private WeaponHudView _weaponHudView;

        public override void InstallBindings()
        {
            PlayerHudInstall();
            WeaponHudInstall();
        }

        private void PlayerHudInstall()
        {
            Container
                .Bind<PlayerHudView>()
                .FromInstance(_playerHudView)
                .AsSingle();
        }

        private void WeaponHudInstall()
        {
            Container
                .Bind<WeaponHudView>()
                .FromInstance(_weaponHudView)
                .AsSingle();
        }
    }
}