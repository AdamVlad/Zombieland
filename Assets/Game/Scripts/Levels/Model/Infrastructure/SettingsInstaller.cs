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

        private void Awake()
        {
            if (_gameSettings == null)
            {
                throw new NullReferenceException("Game settings not set");
            }
            if (_sceneSettings == null)
            {
                throw new NullReferenceException("Scene settings not set");
            }
        }

        public override void InstallBindings()
        {
            GameSettingsInstall();
            SceneSettingsInstall();
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
    }
}
