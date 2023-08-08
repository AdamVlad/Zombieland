using Assets.Game.Scripts.Levels.View.Widgets;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.Levels.View
{
    internal sealed class PlayerHudView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        public Image Icon => _icon;

        [SerializeField] private PlayerHpWidget _playerHpWidget;
        public PlayerHpWidget PlayerHpWidget => _playerHpWidget;
    }
}