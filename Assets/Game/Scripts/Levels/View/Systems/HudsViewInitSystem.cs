using Assets.Game.Scripts.Levels.Model.Components.Data.Player;
using Assets.Game.Scripts.Levels.View.Widgets;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;

using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Assets.Game.Scripts.Levels.View.Systems
{
    internal sealed class HudsViewInitSystem : IEcsInitSystem
    {
        [Inject] private readonly PlayerHudView _playerHudView;

        private readonly EcsFilterInject
            <Inc<PlayerTagComponent>> _playerFilter = default;

        public void Init(IEcsSystems systems)
        {
            if (!_playerFilter.GetFirstEntity(out var playerEntity)) return;

            _playerHudView.PlayerHpWidget.OnInit(1, systems.GetWorld());
            _playerHudView.PlayerHpWidget.BindWidget(systems.GetWorld(), playerEntity);
        }
    }
}