using Assets.Plugins.IvaLib.LeoEcsLite.EcsPhysics.Checkers;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Components.Data.Charges
{
    [RequireComponent(
        typeof(Rigidbody),
        typeof(OnTriggerEnterChecker))]
    internal class Charge : MonoBehaviour
    {
        public ChargeType Type;
        public float Lifetime;
        [HideInInspector] public int Entity;
    }
}