﻿using System.Runtime.CompilerServices;
using Assets.Game.Scripts.Model.AppData;
using Assets.Game.Scripts.Model.Components;
using Assets.Game.Scripts.Model.Components.Delayed;
using Assets.Game.Scripts.Model.Components.Events;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsDelay;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Game.Scripts.Model.Systems.Weapons
{
    internal sealed class WeaponSetSpawnTimeSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<WeaponSpawnerComponent>> _weaponSpawnersFilter = default;
        private readonly EcsPoolInject<Delayed<WeaponSpawnDelayed>> _timerPool = default;

        private readonly EcsSharedInject<SharedData> _sharedData = default;
        private readonly EcsWorldInject _world = default;
        
        public void Run(IEcsSystems systems)
        {
            var eventsBus = _sharedData.Value.EventsBus;
            if (!eventsBus.HasEventSingleton<PlayerPickUpWeaponEvent>(out var eventBody)) return;

            foreach (var spawnEntity in _weaponSpawnersFilter.Value)
            {
                ref var weaponSpawnerComponent = ref _weaponSpawnersFilter.Get1(spawnEntity);

                if (weaponSpawnerComponent.SpawnedWeaponEntity == eventBody.WeaponEntity)
                {
                    weaponSpawnerComponent.SpawnedWeaponEntity = -1;
                    SetSpawnTime(spawnEntity, 5f);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetSpawnTime(int targetEntity, float time)
        {
            var delayedEntity = _world.NewEntity();
            ref var timer = ref _timerPool.Add(delayedEntity);
            timer.TimeLeft = time;
            timer.Target = _world.PackEntity(targetEntity);
        }
    }
}