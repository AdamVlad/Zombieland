using Assets.Game.Scripts.Model.AppData;
using Assets.Game.Scripts.Model.Components;
using Assets.Game.Scripts.Model.Components.Events;
using Assets.Game.Scripts.Model.Components.Items;
using Assets.Plugins.IvaLeoEcsLite.EcsPhysics.Events;
using Assets.Plugins.IvaLeoEcsLite.UnityEcsComponents.EntityReference;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Runtime.CompilerServices;

namespace Assets.Game.Scripts.Model.Systems.Player
{
    internal sealed class PlayerItemPickupSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<PlayerTagComponent, OnTriggerEnterEvent>> _filter = default;

        private readonly EcsSharedInject<SharedData> _sharedData = default;

        private readonly EcsPoolInject<WeaponComponent> _weaponComponentPool = default;

        public void Run(IEcsSystems systems)
        {
            var filterPools = _filter.Pools;

            foreach (var playerEntity in _filter.Value)
            {
                ref var triggerEnterEvent = ref filterPools.Inc2.Get(playerEntity);

                if (!triggerEnterEvent.OtherCollider.TryGetComponent<EntityReference>(out var otherEntityReference)) return;

                if (!otherEntityReference.Unpack(out var otherEntity)) return;

                if (_weaponComponentPool.Value.Has(otherEntity))
                {
                    WeaponPickedUpSendEvent(playerEntity, otherEntity);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void WeaponPickedUpSendEvent(int playerEntity, int weaponEntity)
        {
            _sharedData.Value.EventsBus.NewEventSingleton<PlayerPickUpWeaponEvent>() =
                new PlayerPickUpWeaponEvent
                {
                    PlayerEntity = playerEntity,
                    WeaponEntity = weaponEntity
                };
        }
    }
}