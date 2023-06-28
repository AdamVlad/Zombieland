using Assets.Plugins.IvaLib.LeoEcsLite.EcsPhysics.Emitter;
using UnityEngine;

namespace Assets.Plugins.IvaLib.LeoEcsLite.EcsPhysics.Checkers
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