using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Components.Data.Requests
{
    public struct UpdateWidgetRequest<TWidget, TValue> where TWidget : MonoBehaviour
    {
        public TValue Value;
    }
}