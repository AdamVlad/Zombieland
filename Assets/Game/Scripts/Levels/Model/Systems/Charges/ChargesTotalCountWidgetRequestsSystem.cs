using System.Runtime.CompilerServices;

using Assets.Game.Scripts.Levels.Model.Components.Data.Events;
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
    internal sealed class ChargesTotalCountWidgetRequestsSystem : IEcsRunSystem
    {
        [Inject] private readonly EventsBus _eventsBus;

        private readonly EcsPoolInject<WeaponClipComponent> _weaponClipPool = default;
        private readonly EcsPoolInject<MonoLink<TotalChargesCountWidget>> _totalChargesWidgetPool = default;
        private readonly EcsPoolInject<UpdateWidgetRequest<TotalChargesCountWidget, int>> _updateTotalChargesWidgetRequestPool = default;

        public void Run(IEcsSystems systems)
        {
            if (_eventsBus.HasEventSingleton<PlayerPickUpWeaponEvent>(out var pickupEventBody))
            {
                SetUpdateRequest(pickupEventBody.PlayerEntity, pickupEventBody.WeaponEntity);
            }
            if (_eventsBus.HasEventSingleton<PlayerReloadingEvent>(out var reloadingEventBody))
            {
                SetUpdateRequest(reloadingEventBody.PlayerEntity, reloadingEventBody.WeaponEntity);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetUpdateRequest(int playerEntity, int weaponEntity)
        {
            if (!_totalChargesWidgetPool.Has(playerEntity)) return;
            if (!_weaponClipPool.Has(weaponEntity)) return;
            if (_updateTotalChargesWidgetRequestPool.Has(playerEntity)) return;

            ref var clipComponent = ref _weaponClipPool.Get(weaponEntity);

            _updateTotalChargesWidgetRequestPool.Value.Add(playerEntity) =
                new UpdateWidgetRequest<TotalChargesCountWidget, int>
                {
                    Value = clipComponent.RestChargeCount
                };
        }
    }
}