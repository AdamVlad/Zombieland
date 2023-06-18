using UnityEngine;

namespace Assets.Plugins.IvaLeoEcsLite.EcsPhysics.Events
{
    public struct OnCollisionEnterEvent
    {
        public GameObject SenderGameObject;
        public Collider OtherCollider;
        public ContactPoint FirstContactPoint;
        public Vector3 RelativeVelocity;
    }
}