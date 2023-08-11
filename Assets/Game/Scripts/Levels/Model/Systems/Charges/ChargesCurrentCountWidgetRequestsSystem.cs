using System.Runtime.CompilerServices;

using Assets.Game.Scripts.Levels.Model.Components.Data.Events;
using Assets.Game.Scripts.Levels.Model.Components.Data.Events.Charges;
using Assets.Game.Scripts.Levels.Model.Components.Data.Requests;
using Assets.Game.Scripts.Levels.Model.Components.Data.Weapons;
using Assets.Game.Scripts.Levels.View.Widgets;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;

using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Systems.Charges
{
    internal sealed class ChargesCurrentCountWidgetRequestsSystem : IEcsRunSystem
    {
        [Inject] private readonly EventsBus _eventsBus;

        private readonly EcsPoolInject<WeaponClipComponent> _weaponClipPool = default;
        private readonly EcsPoolInject<MonoLink<CurrentChargesCountWidget>> _currentChargesWidgetPool = default;
        private readonly EcsPoolInject<UpdateWidgetRequest<CurrentChargesCountWidget, int>> _updateCurrentChargesWidgetRequestPool = default;

        public void Run(IEcsSystems systems)
        {
            if (_eventsBus.HasEventSingleton<PlayerPickUpWeaponEvent>(out var pickupEventBody))
            {
                SetUpdateRequest(pickupEventBody.PlayerEntity, pickupEventBody.WeaponEntity);
            }

            foreach (var eventEntity in _eventsBus.GetEventBodies<ChargeGetFromPoolEvent>(out var eventPool))
            {
                var shootEventBody = eventPool.Get(eventEntity);
                SetUpdateRequest(shootEventBody.PlayerEntity, shootEventBody.WeaponEntity);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetUpdateRequest(int playerEntity, int weaponEntity)
        {
            if (!_currentChargesWidgetPool.Has(playerEntity)) return;
            if (!_weaponClipPool.Has(weaponEntity)) return;
            if (_updateCurrentChargesWidgetRequestPool.Has(playerEntity)) return;

            ref var clipComponent = ref _weaponClipPool.Get(weaponEntity);

            _updateCurrentChargesWidgetRequestPool.Value.Add(playerEntity) =
                new UpdateWidgetRequest<CurrentChargesCountWidget, int>
                {
                    Value = clipComponent.CurrentChargeInClipCount
                };
        }
    }
}