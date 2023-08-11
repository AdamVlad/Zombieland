using Assets.Game.Scripts.Levels.Model.Components.Data.Events;
using Assets.Game.Scripts.Levels.Model.Components.Data.Requests;
using Assets.Game.Scripts.Levels.Model.Components.Data.Weapons;
using Assets.Game.Scripts.Levels.View.Widgets;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;

using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Systems.Weapons
{
    internal sealed class WeaponIconWidgetRequestsSystem : IEcsRunSystem
    {
        [Inject] private readonly EventsBus _eventsBus;

        private readonly EcsPoolInject<MonoLink<Weapon>> _weaponPool = default;
        private readonly EcsPoolInject<MonoLink<WeaponIconWidget>> _weaponIconWidgetPool = default;
        private readonly EcsPoolInject<UpdateWidgetRequest<WeaponIconWidget, Sprite>> _updateWeaponIconWidgetRequestPool = default;

        public void Run(IEcsSystems systems)
        {
            if (!_eventsBus.HasEventSingleton<PlayerPickUpWeaponEvent>(out var eventBody)) return;
            if (!_weaponIconWidgetPool.Has(eventBody.PlayerEntity)) return;
            if (_updateWeaponIconWidgetRequestPool.Has(eventBody.PlayerEntity)) return;

            var weapon = _weaponPool.Get(eventBody.WeaponEntity).Value;

            _updateWeaponIconWidgetRequestPool.Value.Add(eventBody.PlayerEntity) =
                new UpdateWidgetRequest<WeaponIconWidget, Sprite>
                {
                    Value = weapon.Settings.Icon
                };
        }
    }
}