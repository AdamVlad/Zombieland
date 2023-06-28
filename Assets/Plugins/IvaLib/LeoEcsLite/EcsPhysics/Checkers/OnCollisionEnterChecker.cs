using Assets.Plugins.IvaLib.LeoEcsLite.EcsPhysics.Emitter;
using UnityEngine;

namespace Assets.Plugins.IvaLib.LeoEcsLite.EcsPhysics.Checkers
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