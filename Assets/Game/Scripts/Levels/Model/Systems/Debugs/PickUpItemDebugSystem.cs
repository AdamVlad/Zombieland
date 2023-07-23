using Assets.Game.Scripts.Levels.Model.Components.Events;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Systems.Debugs
{
    internal sealed class PickUpItemDebugSystem : IEcsRunSystem
    {
        [Inject] private EventsBus _eventsBus;

        public void Run(IEcsSystems systems)
        {
            if (!_eventsBus.HasEventSingleton<PlayerPickUpWeaponEvent>(out var eventBody)) return;

            Debug.Log("[ Event: PlayerPickUpWeaponEvent ] " +
                                  $"[ Sender entity: {eventBody.PlayerEntity} ] " +
                                  $"[ PoolingObject entity: {eventBody.WeaponEntity} ]");
        }
    }
}