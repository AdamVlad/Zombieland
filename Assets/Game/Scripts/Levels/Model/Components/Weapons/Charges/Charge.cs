using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Components.Weapons.Charges
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