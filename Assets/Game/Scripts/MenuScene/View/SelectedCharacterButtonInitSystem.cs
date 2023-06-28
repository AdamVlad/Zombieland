using Assets.Game.Scripts.MenuScene.Model.AppData;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Unity.Ugui;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.MenuScene.View
{
    internal sealed class SelectedCharacterButtonInitSystem : IEcsInitSystem
    {
        [EcsUguiNamed(UiIdents.LeftButtonName)] private Button _leftButton;
        [EcsUguiNamed(UiIdents.RightButtonName)] private Button _rightButton;

        public void Init(IEcsSystems systems)
        {
            Debug.Log(_leftButton.tag);
            Debug.Log(_rightButton.tag);
        }
    }
}