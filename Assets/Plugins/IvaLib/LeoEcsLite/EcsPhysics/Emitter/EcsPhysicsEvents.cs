using Assets.Plugins.IvaLib.LeoEcsLite.EcsPhysics.Events;
using Leopotam.EcsLite;
using UnityEngine;

namespace Assets.Plugins.IvaLib.LeoEcsLite.EcsPhysics.Emitter
{
    public static class EcsPhysicsEvents
    {
        public static EcsWorld World;

        public static void RegisterTriggerEnterEvent(int senderEntity, Collider collider)
        {
            var pool = World.GetPool<OnTriggerEnterEvent>();

            if (pool.Has(senderEntity)) return;

            ref var eventComponent = ref pool.Add(senderEntity);

            eventComponent.SenderEntity = senderEntity;
            eventComponent.OtherCollider = collider;
        }

        public static void RegisterCollisionEnterEvent(int senderEntity, Collider collider)
        {
            var pool = World.GetPool<OnCollisionEnterEvent>();

            if (pool.Has(senderEntity)) return;

            ref var eventComponent = ref pool.Add(senderEntity);

            eventComponent.SenderEntity = senderEntity;
            eventComponent.OtherCollider = collider;
        }
    }
}
