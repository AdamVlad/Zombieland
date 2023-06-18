using Assets.Plugins.IvaLeoEcsLite.UnityEcsComponents.EntityReference;
using UnityEngine;

namespace Assets.Plugins.IvaLeoEcsLite.EcsPhysics.Checkers
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