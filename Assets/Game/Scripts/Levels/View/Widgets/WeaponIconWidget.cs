using Leopotam.EcsLite;

using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.Levels.View.Widgets
{
    internal class WeaponIconWidget : WidgetBase, IStatView<Sprite>
    {
        [SerializeField] private Image _icon;

        public void OnInit(Sprite sprite, EcsWorld world)
        {
            _icon.sprite = sprite;
        }

        public void OnUpdate(Sprite sprite, EcsWorld world)
        {
            _icon.sprite = sprite;
        }
    }
}