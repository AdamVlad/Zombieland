using Assets.Game.Scripts.Model.AppData;
using Assets.Game.Scripts.Model.Components;
using Assets.Game.Scripts.Model.Components.Events;
using Assets.Plugins.IvaLib.LeoEcsLite.Extensions;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Game.Scripts.Model.Systems.Player
{
    internal sealed class PlayerWeaponDropSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<PlayerTagComponent,
                ShootingComponent,
                BackpackComponent>> _filter = default;

        private readonly EcsSharedInject<SharedData> _sharedData = default;

        private readonly EcsPoolInject<MonoLink<Transform>> _transformComponentPool = default;

        public void Run(IEcsSystems systems)
        {
            var eventsBus = _sharedData.Value.EventsBus;
            if (!eventsBus.HasEventSingleton<PlayerPickUpWeaponEvent>(out var eventBody)) return;

            foreach (var playerEntity in _filter.Value)
            {
                ref var shootingComponent = ref _filter.Get2(playerEntity);
                ref var backpackComponent = ref _filter.Get3(playerEntity);

                if (backpackComponent.IsWeaponInHand)
                {
                    ref var transform = ref _transformComponentPool.Get(backpackComponent.WeaponEntity).Value;
                    transform.gameObject.SetActive(false);
                    //transform.position += shootingComponent.Direction * 20;
                    backpackComponent.WeaponEntity = -1;
                }
            }
        }
    }
}