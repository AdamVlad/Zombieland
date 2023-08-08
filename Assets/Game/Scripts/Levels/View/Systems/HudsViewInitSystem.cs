using Assets.Game.Scripts.Levels.Model.Components.Data.Player;
using Assets.Game.Scripts.Levels.View.Widgets;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;

using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Assets.Game.Scripts.Levels.View.Systems
{
    // Разбить на методы
    internal sealed class HudsViewInitSystem : IEcsInitSystem
    {
        [Inject] private readonly PlayerHudView _playerHudView;

        private readonly EcsFilterInject<Inc<MonoLink<Player>>> _playerFilter = default;

        public void Init(IEcsSystems systems)
        {
            if (!_playerFilter.GetFirstEntity(out var playerEntity)) return;

            var playerComponent = _playerFilter.Get1(playerEntity).Value;
            _playerHudView.Icon.sprite = playerComponent.Settings.Icon;

            _playerHudView.PlayerHpWidget.OnInit(1, systems.GetWorld());
            _playerHudView.PlayerHpWidget.BindWidget(systems.GetWorld(), playerEntity);
        }
    }
}