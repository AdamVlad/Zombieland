using Assets.Game.Scripts.Levels.Model.Components.Behaviours.Interfaces;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Components.Behaviours.Base
{
    [RequireComponent(typeof(BehavioursScope))]
    internal abstract class CollisionBehaviourBase : MonoBehaviour, IBehaviour
    {
        protected void Initialize(
            uint maxHittedCount,
            float collisionRadius,
            LayerMask collisionLayers)
        {
            HittedColliders = new Collider[maxHittedCount];
            CollisionRadius = collisionRadius;
            CollisionLayers = collisionLayers;
        }
        
        public abstract float Evaluate();

        public abstract void Behave();

        protected Collider[] HittedColliders;
        protected float CollisionRadius;
        protected LayerMask CollisionLayers;
    }
}