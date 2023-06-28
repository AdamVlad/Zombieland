using UnityEngine;

namespace Assets.Plugins.IvaLib.LeoEcsLite.EcsPhysics.Events
{
    public struct OnCollisionEnterEvent
    {
        public int SenderEntity;
        public Collider OtherCollider;
    }
}