using Assets.Game.Scripts.Model.AppData;
using Assets.Game.Scripts.Model.Components.Events;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Game.Scripts.Model.Systems.Debug
{
    internal sealed class PickUpItemDebugSystem : IEcsRunSystem
    {
        private readonly EcsSharedInject<SharedData> _sharedData;

        public void Run(IEcsSystems systems)
        {
            var eventsBus = _sharedData.Value.EventsBus;
            if (!eventsBus.HasEventSingleton<PlayerPickUpWeaponEvent>(out var eventBody)) return;

            UnityEngine.Debug.Log("[ Event: PlayerPickUpWeaponEvent ] " +
                                  $"[ Sender entity: {eventBody.PlayerEntity} ] " +
                                  $"[ Weapon entity: {eventBody.WeaponEntity} ]");
        }
    }
}