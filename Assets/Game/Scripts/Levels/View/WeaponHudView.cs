using Assets.Game.Scripts.Levels.View.Widgets;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.View
{
    internal sealed class WeaponHudView : MonoBehaviour
    {
        [SerializeField] private WeaponIconWidget _iconWidget;
        public WeaponIconWidget IconWidget => _iconWidget;

        [SerializeField] private CurrentChargesCountWidget _currentChargesCountWidget;
        public CurrentChargesCountWidget CurrentChargesCountWidget => _currentChargesCountWidget;

        [SerializeField] private TotalChargesCountWidget _totalChargesCountWidget;
        public TotalChargesCountWidget TotalChargesCountWidget => _totalChargesCountWidget;
    }
}