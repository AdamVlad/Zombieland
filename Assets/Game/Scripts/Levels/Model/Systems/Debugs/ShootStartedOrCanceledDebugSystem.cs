using Assets.Game.Scripts.Levels.Model.AppData;
using Assets.Game.Scripts.Levels.Model.Components.Events.Shoot;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Systems.Debugs
{
    internal sealed class ShootStartedOrCanceledDebugSystem : IEcsRunSystem
    {
        private readonly EcsSharedInject<SharedData> _sharedData = default;

        public void Run(IEcsSystems systems)
        {
            var eventsBus = _sharedData.Value.EventsBus;
            if (eventsBus.HasEventSingleton<ShootStartedEvent>())
            {
                Log<ShootStartedEvent>();
            }
            if (eventsBus.HasEventSingleton<ShootEndedEvent>())
            {
                Log<ShootEndedEvent>();
            }
        }

        private void Log<T>()
        {
            Debug.Log($"[ Event: {typeof(T).Name}");
        }
    }
}