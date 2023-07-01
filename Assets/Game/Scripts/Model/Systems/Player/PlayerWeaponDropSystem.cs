﻿using Assets.Game.Scripts.Model.AppData;
using Assets.Game.Scripts.Model.Components;
using Assets.Game.Scripts.Model.Components.Delayed;
using Assets.Game.Scripts.Model.Components.Events;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsDelay;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Assets.Game.Scripts.Model.Systems.Player
{
    internal sealed class PlayerWeaponDropSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<PlayerTagComponent,
                BackpackComponent>> _filter = default;

        private readonly EcsSharedInject<SharedData> _sharedData = default;

        private readonly EcsWorldInject _world = default;

        private readonly EcsPoolInject<MonoLink<Transform>> _transformComponentPool = default;
        private readonly EcsPoolInject<MonoLink<Rigidbody>> _rigidbodyComponentPool = default;
        private readonly EcsPoolInject<ParentComponent> _parentComponentPool = default;
        private readonly EcsPoolInject<Delayed<DestructionDelayed>> _timerPool = default;

        public void Run(IEcsSystems systems)
        {
            var eventsBus = _sharedData.Value.EventsBus;
            if (!eventsBus.HasEventSingleton<PlayerPickUpWeaponEvent>(out _)) return;

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
                    SetDestructionTime(backpackComponent.WeaponEntity, 0.8f);

                    backpackComponent.WeaponEntity = -1;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetDestructionTime(int targetEntity, float time)
        {
            var delayedEntity = _world.NewEntity();
            ref var timer = ref _timerPool.Add(delayedEntity);
            timer.TimeLeft = time;
            timer.Target = _world.PackEntity(targetEntity);
        }
    }
}