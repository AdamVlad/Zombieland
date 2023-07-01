using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsPhysics.Events;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Game.Scripts.Model.Systems.Debugs
{
    internal sealed class CollisionEnterDebugSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<OnCollisionEnterEvent>> _filter = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var collisionEnterEvent = ref _filter.Get1(entity);

                Debug.Log("[ Event: OnCollisionEnterEvent ] " +
                                      $"[ Sender: {collisionEnterEvent.SenderEntity} ] " +
                                      $"[ Other: {collisionEnterEvent.OtherCollider.gameObject.name} ]");
            }
        }
    }
}