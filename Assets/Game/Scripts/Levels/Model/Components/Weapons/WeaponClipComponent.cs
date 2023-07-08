using Assets.Game.Scripts.Levels.Model.Components.Weapons.Charges;
using UnityEngine.Pool;

namespace Assets.Game.Scripts.Levels.Model.Components.Weapons
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

        public IObjectPool<Bullet> BulletsPool;

        public WeaponClipComponent(
            ChargeType chargeType,
            int totalCharge,
            int clipCapacity,
            IObjectPool<Bullet> bulletsPool = null)
        {
            _chargeType = chargeType;
            _totalCharge = totalCharge;
            _clipCapacity = clipCapacity;
            CurrentChargeInClipCount = _clipCapacity;
            RestChargeCount = _totalCharge - _clipCapacity;
            BulletsPool = bulletsPool;
        }
    }
}