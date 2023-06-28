using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents.EntityReference;
using UnityEngine;

namespace Assets.Plugins.IvaLib.LeoEcsLite.EcsPhysics.Checkers
{
    [RequireComponent(typeof(EntityReference))]
    public abstract class PhysicsCheckerBase : MonoBehaviour
    {
        protected void Start()
        {
            _entityReference = GetComponent<EntityReference>();
        }

        protected EntityReference _entityReference;
    }
}