using Assets.Game.Scripts.Model.AppData;
using Assets.Game.Scripts.Model.Components.Events.Input;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Game.Scripts.Model.Systems.Debugs
{
    internal sealed class ShootStartedOrCanceledDebugSystem : IEcsRunSystem
    {
        private readonly EcsSharedInject<SharedData> _sharedData;

        public void Run(IEcsSystems systems)
        {
            var eventsBus = _sharedData.Value.EventsBus;
            if (eventsBus.HasEventSingleton<InputShootStartedEvent>())
            {
                Log<InputShootStartedEvent>();
            }
            if (eventsBus.HasEventSingleton<InputShootEndedEvent>())
            {
                Log<InputShootEndedEvent>();
            }
        }

        private void Log<T>()
        {
            Debug.Log($"[ Event: {typeof(T).Name}");
        }
    }
}