using Assets.Game.Scripts.MenuScene.Model.AppData;
using Leopotam.EcsLite.Unity.Ugui;
using UnityEngine.Scripting;
using UnityEngine;

namespace Assets.Game.Scripts.MenuScene.View
{
    public class ChangeHeroButtonClickEventSystem : EcsUguiCallbackSystem
    {
        [Preserve]
        [EcsUguiClickEvent(UiIdents.LeftButtonName)]
        private void OnLeftButtonClick(in EcsUguiClickEvent evt)
        {
            Debug.Log("Left....", evt.Sender);
        }

        [Preserve]
        [EcsUguiClickEvent(UiIdents.RightButtonName)]
        private void OnRightButtonClick(in EcsUguiClickEvent evt)
        {
            Debug.Log("Right....", evt.Sender);
        }
    }
}
