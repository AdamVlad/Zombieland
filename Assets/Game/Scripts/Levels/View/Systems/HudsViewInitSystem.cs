using Assets.Game.Scripts.Levels.Model.Components.Data;
using Assets.Game.Scripts.Levels.Model.Components.Data.Player;
using Assets.Game.Scripts.Levels.Model.Components.Data.Weapons;
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
        [Inject] private readonly EcsWorld _world;
        [Inject] private readonly PlayerHudView _playerHudView;
        [Inject] private readonly WeaponHudView _weaponHudView;

        private readonly EcsFilterInject
            <Inc<MonoLink<Player>,
                BackpackComponent>> _playerFilter = default;

        private readonly EcsPoolInject<MonoLink<Weapon>> _weaponPool = default;

        public void Init(IEcsSystems systems)
        {
            if (!_playerFilter.GetFirstEntity(out var playerEntity)) return;

            var playerComponent = _playerFilter.Get1(playerEntity).Value;
            var backpackComponent = _playerFilter.Get2(playerEntity);
            var weapon = _weaponPool.Get(backpackComponent.WeaponEntity).Value;

            _playerHudView.Icon.sprite = playerComponent.Settings.Icon;
            _playerHudView.PlayerHpWidget.OnInit(1, _world);
            _playerHudView.PlayerHpWidget.BindWidget(_world, playerEntity);

            _weaponHudView.CurrentChargesCountWidget.BindWidget(_world, playerEntity);
            _weaponHudView.IconWidget.OnInit(weapon.Settings.Icon, _world);
            _weaponHudView.CurrentChargesCountWidget.OnInit(weapon.Settings.ClipCapacity, _world);
            _weaponHudView.TotalChargesCountWidget.OnInit(weapon.Settings.ClipCapacity, _world);
        }
    }
}