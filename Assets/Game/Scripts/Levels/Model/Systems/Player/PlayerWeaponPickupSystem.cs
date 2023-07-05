using Assets.Game.Scripts.Levels.Model.AppData;
using Assets.Game.Scripts.Levels.Model.Components;
using Assets.Game.Scripts.Levels.Model.Components.Events;
using Assets.Game.Scripts.Levels.Model.Components.Requests;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Systems.Player
{
    internal sealed class PlayerWeaponPickupSystem : IEcsRunSystem
    {
        private readonly EcsSharedInject<SharedData> _sharedData = default;

        private readonly EcsPoolInject<BackpackComponent> _backpackComponentPool = default;
        private readonly EcsPoolInject<MonoLink<Transform>> _transformComponentPool = default;
        private readonly EcsPoolInject<MonoLink<Collider>> _colliderComponentPool = default;
        private readonly EcsPoolInject<ParentComponent> _parentComponentPool = default;
        private readonly EcsPoolInject<WeaponAnimationStopRequest> _weaponAnimationRequestPool = default;

        public void Run(IEcsSystems systems)
        {
            var eventsBus = _sharedData.Value.EventsBus;
            if (!eventsBus.HasEventSingleton<PlayerPickUpWeaponEvent>(out var eventBody)) return;

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