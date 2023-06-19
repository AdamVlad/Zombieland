using Assets.Plugins.IvaLeoEcsLite.EcsEvents;
using Assets.Plugins.IvaLeoEcsLite.EcsPhysics.Events;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Game.Scripts.Model.Systems.Debugs
{
    internal sealed class TriggerEnterDebugSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<OnTriggerEnterEvent>> _filter = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var pools = _filter.Pools;

                ref var triggerEnterEvent = ref pools.Inc1.Get(entity);

                Debug.Log("[ Event: OnTriggerEnterEvent ] " +
                                      $"[ Sender: {triggerEnterEvent.SenderGameObject.name} ] " +
                                      $"[ Other: {triggerEnterEvent.OtherCollider.gameObject.name} ]");
            }
        }
    }
}