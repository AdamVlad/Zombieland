using System.Runtime.CompilerServices;
using Assets.Game.Scripts.Levels.Model.Components;
using Assets.Game.Scripts.Levels.Model.Components.Delayed;
using Assets.Game.Scripts.Levels.Model.Components.Events;
using Assets.Game.Scripts.Levels.Model.ScriptableObjects;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsDelay;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Systems.Weapons
{
    internal sealed class WeaponSetSpawnTimeSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<WeaponSpawnerComponent>> _weaponSpawnersFilter = default;
        private readonly EcsPoolInject<DelayedAdd<WeaponSpawnDelayed>> _timerPool = default;

        [Inject] private SceneConfigurationSo _sceneSettings;
        [Inject] private EventsBus _eventsBus;
        [Inject] private EcsWorld _world;

        public void Run(IEcsSystems systems)
        {
            if (!_eventsBus.HasEventSingleton<PlayerPickUpWeaponEvent>(out var eventBody)) return;

            foreach (var spawnEntity in _weaponSpawnersFilter.Value)
            {
                ref var weaponSpawnerComponent = ref _weaponSpawnersFilter.Get1(spawnEntity);

                if (weaponSpawnerComponent.SpawnedWeaponEntity == eventBody.WeaponEntity)
                {
                    weaponSpawnerComponent.SpawnedWeaponEntity = -1;
                    SetSpawnTime(spawnEntity, _sceneSettings.WeaponSpawnDelay);
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