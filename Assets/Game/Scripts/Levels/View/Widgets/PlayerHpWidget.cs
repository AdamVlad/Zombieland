using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.Levels.View.Widgets
{
    internal class PlayerHpWidget : WidgetBase, IStatView<float>
    {
        [SerializeField] private Image _hpImageFill;

        public void OnInit(float amount, EcsWorld world)
        {
            _hpImageFill.fillAmount = amount;
        }

        public void OnUpdate(float amount, EcsWorld world)
        {
            _hpImageFill.fillAmount = amount;
        }
    }
}