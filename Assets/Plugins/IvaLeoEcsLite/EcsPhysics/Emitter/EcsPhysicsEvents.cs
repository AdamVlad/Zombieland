using Assets.Plugins.IvaLeoEcsLite.EcsPhysics.Events;
using Leopotam.EcsLite;
using UnityEngine;

namespace Assets.Plugins.IvaLeoEcsLite.EcsPhysics.Emitter
{
    public static class EcsPhysicsEvents
    {
        public static EcsWorld World;

        public static void RegisterTriggerEnterEvent(GameObject senderGameObject, int senderEntity, Collider collider)
        {
            var pool = World.GetPool<OnTriggerEnterEvent>();

            if (pool.Has(senderEntity)) return;

            ref var eventComponent = ref pool.Add(senderEntity);

            eventComponent.SenderGameObject = senderGameObject;
            eventComponent.OtherCollider = collider;
        }

        public static void RegisterCollisionEnterEvent(GameObject senderGameObject, int senderEntity, Collider collider, ContactPoint firstContactPoint, Vector3 relativeVelocity)
        {
            var pool = World.GetPool<OnCollisionEnterEvent>();

            if (pool.Has(senderEntity)) return;

            ref var eventComponent = ref pool.Add(senderEntity);

            eventComponent.SenderGameObject = senderGameObject;
            eventComponent.OtherCollider = collider;
            eventComponent.FirstContactPoint = firstContactPoint;
            eventComponent.RelativeVelocity = relativeVelocity;
        }
    }
}
