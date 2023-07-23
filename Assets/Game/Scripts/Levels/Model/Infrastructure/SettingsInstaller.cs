using System;
using Assets.Game.Scripts.Levels.Model.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Infrastructure
{
    internal sealed class SettingsInstaller : MonoInstaller
    {
        [SerializeField] private GameConfigurationSo _gameSettings;
        [SerializeField] private SceneConfigurationSo _sceneSettings;
        [SerializeField] private PlayerConfigurationSo _playerSettings;

        private void Awake()
        {
            if (_gameSettings == null)
            {
                throw new NullReferenceException("Player settings not set");
            }
        }

        public override void InstallBindings()
        {
            GameSettingsInstall();
            SceneSettingsInstall();
            PlayerSettingsInstall();
        }

        private void GameSettingsInstall()
        {
            Container
                .Bind<GameConfigurationSo>()
                .FromScriptableObject(_gameSettings)
                .AsSingle();
        }

        private void SceneSettingsInstall()
        {
            Container
                .Bind<SceneConfigurationSo>()
                .FromScriptableObject(_sceneSettings)
                .AsSingle();
        }

        private void PlayerSettingsInstall()
        {
            Container
                .Bind<PlayerConfigurationSo>()
                .FromScriptableObject(_playerSettings)
                .AsSingle();
        }
    }
}
