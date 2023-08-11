using Assets.Game.Scripts.Levels.Model.Components.Data.Charges;
using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Game.Scripts.Levels.Model.Components.Data.Weapons
{
    internal struct WeaponClipComponent
    {
        private ChargeType _chargeType;
        public ChargeType ChargeType => _chargeType;

        private int _totalCharge;
        public int TotalCharge => _totalCharge;

        private int _clipCapacity;
        public int ClipCapacity => _clipCapacity;

        public int CurrentChargeInClipCount;
        public int RestChargeCount;

        public IObjectPool<Charge> ChargePool;

        public WeaponClipComponent(
            ChargeType chargeType,
            int totalCharge,
            int clipCapacity,
            IObjectPool<Charge> chargePool = null)
        {
            if (totalCharge < clipCapacity)
            {
                Debug.LogError("Total capacity less than clip capacity");
            }

            _chargeType = chargeType;
            _totalCharge = totalCharge;
            _clipCapacity = clipCapacity;
            CurrentChargeInClipCount = _clipCapacity;
            RestChargeCount = _totalCharge - _clipCapacity;
            ChargePool = chargePool;
        }
    }
}