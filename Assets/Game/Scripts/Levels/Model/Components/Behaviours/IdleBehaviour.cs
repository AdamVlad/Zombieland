using Assets.Game.Scripts.Levels.Model.Components.Behaviours.Interfaces;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Components.Behaviours
{
    [RequireComponent(typeof(BehavioursScope))]
    internal sealed class IdleBehaviour : MonoBehaviour, IBehaviour
    {
        public float Evaluate()
        {
            return 0.1f;
        }

        public void Behave()
        {
        }
    }
}