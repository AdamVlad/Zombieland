using UnityEngine;

namespace Assets.Plugins.IvaLeoEcsLite.EcsPhysics.Events
{
    public struct OnTriggerEnterEvent
    {
        public GameObject SenderGameObject;
        public Collider OtherCollider;
    }
}