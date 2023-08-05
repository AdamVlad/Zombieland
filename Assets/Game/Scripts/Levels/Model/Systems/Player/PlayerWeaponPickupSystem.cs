using Assets.Game.Scripts.Levels.Model.Components.Data;
using Assets.Game.Scripts.Levels.Model.Components.Data.Events;
using Assets.Game.Scripts.Levels.Model.Components.Data.Requests;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;

using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Systems.Player
{
    internal sealed class PlayerWeaponPickupSystem : IEcsRunSystem
    {
        [Inject] private readonly EventsBus _eventsBus;

        private readonly EcsPoolInject<BackpackComponent> _backpackComponentPool = default;
        private readonly EcsPoolInject<MonoLink<Transform>> _transformComponentPool = default;
        private readonly EcsPoolInject<MonoLink<Collider>> _colliderComponentPool = default;
        private readonly EcsPoolInject<ParentComponent> _parentComponentPool = default;
        private readonly EcsPoolInject<WeaponAnimationStopRequest> _weaponAnimationRequestPool = default;

        public void Run(IEcsSystems systems)
        {
            if (!_eventsBus.HasEventSingleton<PlayerPickUpWeaponEvent>(out var eventBody)) return;

            _weaponAnimationRequestPool.Add(eventBody.WeaponEntity);

            ref var weaponTransform = ref _transformComponentPool.Get(eventBody.WeaponEntity).Value;
            ref var weaponCollider = ref _colliderComponentPool.Get(eventBody.WeaponEntity).Value;
            ref var weaponParentComponent = ref _parentComponentPool.Get(eventBody.WeaponEntity);
            ref var backpackComponent = ref _backpackComponentPool.Get(eventBody.PlayerEntity);

            weaponTransform.parent = backpackComponent.WeaponHolderPoint;
            weaponTransform.localPosition = Vector3.zero;
            weaponTransform.localRotation = new Quaternion(0, 0, 0, 0);
            weaponCollider.enabled = false;

            backpackComponent.WeaponEntity = eventBody.WeaponEntity;
            weaponParentComponent.CurrentParentTransform = backpackComponent.WeaponHolderPoint;
        }
    }
}