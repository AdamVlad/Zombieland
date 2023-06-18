using Assets.Plugins.IvaLeoEcsLite.EcsPhysics.Events;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Game.Scripts.Model.Systems.Debug
{
    internal sealed class CollisionEnterDebugSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<OnCollisionEnterEvent>> _filter = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var pools = _filter.Pools;

                ref var collisionEnterEvent = ref pools.Inc1.Get(entity);

                UnityEngine.Debug.Log("[ Event: OnCollisionEnterEvent ] " +
                                      $"[ Sender: {collisionEnterEvent.SenderGameObject.name} ] " +
                                      $"[ Other: {collisionEnterEvent.FirstContactPoint.otherCollider.gameObject.name} ]");
            }
        }
    }
}