using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsPhysics.Events;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Systems.Debugs
{
    internal sealed class TriggerEnterDebugSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<OnTriggerEnterEvent>> _filter = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var triggerEnterEvent = ref _filter.Get1(entity);

                Debug.Log("[ Event: OnTriggerEnterEvent ] " +
                                      $"[ Sender: {triggerEnterEvent.SenderEntity} ] " +
                                      $"[ Other: {triggerEnterEvent.OtherCollider.gameObject.name} ]");
            }
        }
    }
}