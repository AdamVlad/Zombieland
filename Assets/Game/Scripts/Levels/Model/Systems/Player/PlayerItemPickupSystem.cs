using System.Runtime.CompilerServices;
using Assets.Game.Scripts.Levels.Model.AppData;
using Assets.Game.Scripts.Levels.Model.Components;
using Assets.Game.Scripts.Levels.Model.Components.Events;
using Assets.Game.Scripts.Levels.Model.Components.Weapons;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsPhysics.Events;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents.EntityReference;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Game.Scripts.Levels.Model.Systems.Player
{
    internal sealed class PlayerItemPickupSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<PlayerTagComponent,
                OnTriggerEnterEvent>> _filter = default;

        private readonly EcsSharedInject<SharedData> _sharedData = default;

        private readonly EcsPoolInject<MonoLink<Weapon>> _weaponComponentPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var playerEntity in _filter.Value)
            {
                ref var triggerEnterEvent = ref _filter.Get2(playerEntity);

                if (!triggerEnterEvent.OtherCollider.TryGetComponent<EntityReference>(out var otherEntityReference)) return;

                if (!otherEntityReference.Unpack(out var otherEntity)) return;

                if (_weaponComponentPool.Has(otherEntity))
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