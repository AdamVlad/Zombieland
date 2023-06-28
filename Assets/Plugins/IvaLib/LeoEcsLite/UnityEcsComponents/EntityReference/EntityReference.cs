using Assets.Plugins.IvaLib.LeoEcsLite.EcsPhysics.Emitter;
using Leopotam.EcsLite;
using UnityEngine;

namespace Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents.EntityReference
{
    [DisallowMultipleComponent]
    public class EntityReference : MonoBehaviour
    {
        private EcsPackedEntity _entityPacked;

        public bool Unpack(out int unpacked)
        {
            return _entityPacked.Unpack(EcsPhysicsEvents.World, out unpacked);
        }

        public void Pack(int entity)
        {
            _entityPacked = EcsPhysicsEvents.World.PackEntity(entity);
        }
    }
}