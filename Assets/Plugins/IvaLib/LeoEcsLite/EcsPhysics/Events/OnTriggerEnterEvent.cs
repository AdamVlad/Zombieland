using UnityEngine;

namespace Assets.Plugins.IvaLib.LeoEcsLite.EcsPhysics.Events
{
    public struct OnTriggerEnterEvent
    {
        public int SenderEntity;
        public Collider OtherCollider;
    }
}