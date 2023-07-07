using Assets.Game.Scripts.Levels.Model.Components.Weapons.Charges;

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

        public int CurrentCharge;
        public int RestCharge;

        public WeaponClipComponent(
            ChargeType chargeType,
            int totalCharge,
            int clipCapacity)
        {
            _chargeType = chargeType;
            _totalCharge = totalCharge;
            _clipCapacity = clipCapacity;
            CurrentCharge = _clipCapacity;
            RestCharge = _totalCharge - _clipCapacity;
        }
    }
}