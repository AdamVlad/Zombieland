using Leopotam.EcsLite;

using TMPro;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.View.Widgets
{
    internal class CurrentChargesCountWidget : WidgetBase, IStatView<int>
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void OnInit(int amount, EcsWorld world)
        {
            _text.text = amount.ToString();
        }

        public void OnUpdate(int amount, EcsWorld world)
        {
            _text.text = amount.ToString();
        }
    }
}