using Assets.Plugins.IvaLib.LeoEcsLite.EcsPhysics.Checkers;
using Assets.Plugins.IvaLib.UnityLib.Pools;

using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Components.Data.Charges
{
    [RequireComponent(
        typeof(Rigidbody),
        typeof(OnTriggerEnterChecker))]
    internal class Charge : PoolsItemBase<Charge>
    {
        public float Lifetime;
        [HideInInspector] public int Entity;
    }
}