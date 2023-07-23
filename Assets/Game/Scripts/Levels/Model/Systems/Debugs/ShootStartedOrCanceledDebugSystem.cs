using Assets.Game.Scripts.Levels.Model.Components.Events.Shoot;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Systems.Debugs
{
    internal sealed class ShootStartedOrCanceledDebugSystem : IEcsRunSystem
    {
        [Inject] private EventsBus _eventsBus;

        public void Run(IEcsSystems systems)
        {
            if (_eventsBus.HasEventSingleton<ShootStartedEvent>())
            {
                Log<ShootStartedEvent>();
            }
            if (_eventsBus.HasEventSingleton<ShootEndedEvent>())
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