using Assets.Game.Scripts.Model.AppData;
using Assets.Game.Scripts.Model.Components;
using Assets.Game.Scripts.Model.Components.Events;
using Assets.Plugins.IvaLeoEcsLite.UnityEcsComponents;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Game.Scripts.Model.Systems.Player
{
    internal sealed class PlayerWeaponPickupSystem : IEcsRunSystem
    {
        private readonly EcsSharedInject<SharedData> _sharedData = default;

        private readonly EcsPoolInject<BackpackComponent> _backpackComponentPool = default;
        private readonly EcsPoolInject<MonoLink<Transform>> _transformComponentPool = default;
        private readonly EcsPoolInject<MonoLink<Collider>> _colliderComponentPool = default;

        public void Run(IEcsSystems systems)
        {
            var eventsBus = _sharedData.Value.EventsBus;
            if (!eventsBus.HasEventSingleton<PlayerPickUpWeaponEvent>(out var eventBody)) return;

            ref var weaponTransform = ref _transformComponentPool.Value.Get(eventBody.WeaponEntity).Value;
            ref var weaponCollider = ref _colliderComponentPool.Value.Get(eventBody.WeaponEntity).Value;
            ref var backpackComponent = ref _backpackComponentPool.Value.Get(eventBody.PlayerEntity);

            weaponTransform.parent = backpackComponent.WeaponHolderPoint;
            weaponTransform.localPosition = new Vector3(-0.046f, 0.206f, 0.028f);
            weaponTransform.localRotation = new Quaternion(0, 0, 0, 0);
            weaponCollider.enabled = false;

            backpackComponent.WeaponEntity = eventBody.WeaponEntity;
        }
    }
}