using Assets.Plugins.IvaLeoEcsLite.EcsPhysics.Emitter;
using UnityEngine;

namespace Assets.Plugins.IvaLeoEcsLite.EcsPhysics.Checkers
{
    public class OnCollisionEnterChecker : PhysicsCheckerBase
    {
        private void OnCollisionEnter(Collision other)
        {
            if (_entityReference.Unpack(out var entity))
            {
                EcsPhysicsEvents.RegisterCollisionEnterEvent(
                    entity,
                    other.collider);
            }
        }
    }
}