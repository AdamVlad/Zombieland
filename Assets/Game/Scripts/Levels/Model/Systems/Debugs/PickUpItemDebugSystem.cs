﻿using Assets.Game.Scripts.Levels.Model.AppData;
using Assets.Game.Scripts.Levels.Model.Components.Events;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Systems.Debugs
{
    internal sealed class PickUpItemDebugSystem : IEcsRunSystem
    {
        private readonly EcsSharedInject<SharedData> _sharedData = default;

        public void Run(IEcsSystems systems)
        {
            if (!_sharedData.Value.EventsBus.HasEventSingleton<PlayerPickUpWeaponEvent>(out var eventBody)) return;

            Debug.Log("[ Event: PlayerPickUpWeaponEvent ] " +
                                  $"[ Sender entity: {eventBody.PlayerEntity} ] " +
                                  $"[ PoolingObject entity: {eventBody.WeaponEntity} ]");
        }
    }
}