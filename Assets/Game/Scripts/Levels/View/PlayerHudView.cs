using Assets.Game.Scripts.Levels.View.Widgets;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.View
{
    internal sealed class PlayerHudView : MonoBehaviour
    {
        [SerializeField] private PlayerHpWidget _playerHpWidget;
        public PlayerHpWidget PlayerHpWidget => _playerHpWidget;
    }
}