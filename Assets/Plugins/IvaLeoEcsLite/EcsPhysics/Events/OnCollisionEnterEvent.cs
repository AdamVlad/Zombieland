using UnityEngine;

namespace Assets.Plugins.IvaLeoEcsLite.EcsPhysics.Events
{
    public struct OnCollisionEnterEvent
    {
        public int SenderEntity;
        public Collider OtherCollider;
    }
}