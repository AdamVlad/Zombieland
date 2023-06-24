using Assets.Plugins.IvaLeoEcsLite.EcsPhysics.Emitter;
using UnityEngine;

namespace Assets.Plugins.IvaLeoEcsLite.EcsPhysics.Checkers
{
    public class OnTriggerEnterChecker : PhysicsCheckerBase
    {
        private void OnTriggerEnter(Collider other)
        {
            if (_entityReference.Unpack(out var entity))
            {
                EcsPhysicsEvents.RegisterTriggerEnterEvent(
                    entity,
                    other);
            }
        }
    }
}