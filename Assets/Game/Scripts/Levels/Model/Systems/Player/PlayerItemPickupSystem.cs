using System.Runtime.CompilerServices;

using Assets.Game.Scripts.Levels.Model.Components.Data.Events;
using Assets.Game.Scripts.Levels.Model.Components.Data.Player;
using Assets.Game.Scripts.Levels.Model.Components.Data.Weapons;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsPhysics.Events;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents.EntityReference;

using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Systems.Player
{
    internal sealed class PlayerItemPickupSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject
            <Inc<PlayerTagComponent,
                OnTriggerEnterEvent>> _filter = default;

        [Inject] private readonly EventsBus _eventsBus;

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
            _eventsBus.NewEventSingleton<PlayerPickUpWeaponEvent>() =
                new PlayerPickUpWeaponEvent
                {
                    PlayerEntity = playerEntity,
                    WeaponEntity = weaponEntity
                };
        }
    }
}