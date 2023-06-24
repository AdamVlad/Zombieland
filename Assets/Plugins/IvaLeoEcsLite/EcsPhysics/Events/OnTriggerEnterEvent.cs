using UnityEngine;

namespace Assets.Plugins.IvaLeoEcsLite.EcsPhysics.Events
{
    public struct OnTriggerEnterEvent
    {
        public int SenderEntity;
        public Collider OtherCollider;
    }
}