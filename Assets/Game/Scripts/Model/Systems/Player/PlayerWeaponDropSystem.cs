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
                BackpackComponent>> _filter = default;

        private readonly EcsSharedInject<SharedData> _sharedData = default;

        private readonly EcsPoolInject<MonoLink<Transform>> _transformComponentPool = default;
        private readonly EcsPoolInject<MonoLink<Rigidbody>> _rigidbodyComponentPool = default;
        private readonly EcsPoolInject<ParentComponent> _parentComponentPool = default;

        public void Run(IEcsSystems systems)
        {
            var eventsBus = _sharedData.Value.EventsBus;
            if (!eventsBus.HasEventSingleton<PlayerPickUpWeaponEvent>(out var eventBody)) return;

            foreach (var playerEntity in _filter.Value)
            {
                ref var backpackComponent = ref _filter.Get2(playerEntity);

                if (backpackComponent.IsWeaponInHand)
                {
                    ref var weaponTransform = ref _transformComponentPool.Get(backpackComponent.WeaponEntity).Value;
                    ref var weaponRigidbody = ref _rigidbodyComponentPool.Get(backpackComponent.WeaponEntity).Value;
                    ref var weaponParentComponent = ref _parentComponentPool.Get(backpackComponent.WeaponEntity);
                    ref var playerTransform = ref _transformComponentPool.Get(playerEntity).Value;

                    weaponTransform.parent = weaponParentComponent.InitParentTransform;
                    weaponParentComponent.CurrentParentTransform = weaponTransform.parent;

                    weaponRigidbody.isKinematic = false;
                    var forceDirection = new Vector3(playerTransform.forward.x, 1, playerTransform.forward.z);
                    weaponRigidbody.AddForce(forceDirection * 150, ForceMode.Force);

                    backpackComponent.WeaponEntity = -1;
                }
            }
        }
    }
}