using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Components.Charges
{
    [RequireComponent(
        typeof(Rigidbody))]
    internal class Charge : MonoBehaviour
    {
        public ChargeType Type;
        public float Lifetime;
        [HideInInspector] public int Entity;
    }
}