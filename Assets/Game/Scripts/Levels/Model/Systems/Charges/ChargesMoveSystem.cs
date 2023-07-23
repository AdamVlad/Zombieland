using Assets.Game.Scripts.Levels.Model.Components;
using Assets.Game.Scripts.Levels.Model.Components.Charges;
using Assets.Game.Scripts.Levels.Model.Components.Events.Charges;
using Assets.Game.Scripts.Levels.Model.Components.Player;
using Assets.Game.Scripts.Levels.Model.Components.Weapons;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;
using DG.Tweening;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Systems.Charges
{
    internal sealed class ChargesMoveSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject
            <Inc<PlayerTagComponent,
                MonoLink<Transform>,
                BackpackComponent>> _playerFilter = default;

        [Inject] private EventsBus _eventsBus;

        private readonly EcsPoolInject<MonoLink<Charge>> _chargePool = default;
        private readonly EcsPoolInject<MonoLink<Transform>> _transformPool = default;
        private readonly EcsPoolInject<WeaponShootingComponent> _weaponShootingPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var eventEntity in _eventsBus.GetEventBodies<ChargeCreatedEvent>(out var chargeCreatedEventPool))
            {
                ref var eventBody = ref chargeCreatedEventPool.Get(eventEntity);

                ref var charge = ref _chargePool.Get(eventBody.Entity).Value;

                foreach (var playerEntity in _playerFilter.Value)
                {
                    ref var playerTransform = ref _transformPool.Get(playerEntity).Value;
                    ref var weaponEntity = ref _playerFilter.Get3(playerEntity).WeaponEntity;
                    ref var weaponShootingComponent = ref _weaponShootingPool.Get(weaponEntity);

                    charge.transform.position = weaponShootingComponent.StartShootingPoint.position;
                    charge.transform.DOMove(
                        charge.transform.position +
                        playerTransform.forward * weaponShootingComponent.ShootingDistance,
                        weaponShootingComponent.ShootingPower);
                }
            }
        }
    }
}