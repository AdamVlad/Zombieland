using Assets.Plugins.IvaLeoEcsLite.EcsPhysics.Events;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Game.Scripts.Model.Systems.Debug
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

                UnityEngine.Debug.Log("[ Event: OnTriggerEnterEvent ] " +
                                      $"[ Sender: {triggerEnterEvent.SenderGameObject.name} ] " +
                                      $"[ Other: {triggerEnterEvent.OtherCollider.gameObject.name} ]");
            }
        }
    }
}