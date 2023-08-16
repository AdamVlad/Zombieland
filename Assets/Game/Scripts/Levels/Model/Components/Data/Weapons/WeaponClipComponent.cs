using Assets.Game.Scripts.Levels.Model.Components.Data.Charges;

using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Game.Scripts.Levels.Model.Components.Data.Weapons
{
    internal struct WeaponClipComponent
    {
        public int ClipCapacity { get; }

        public int CurrentCharge { get; set; }

        public IObjectPool<Charge> ChargePool { get; }

        public WeaponClipComponent(
            int clipCapacity,
            IObjectPool<Charge> pool)
        {
            if (clipCapacity < 0)
            {
                Debug.LogError("Clip capacity can not less than zero");
            }

            ClipCapacity = clipCapacity;
            CurrentCharge = ClipCapacity;
            ChargePool = pool;
        }
    }
}