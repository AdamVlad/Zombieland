using System;

namespace Assets.Game.Scripts.Levels.Model.Components.Weapons.Charges
{
    internal enum ChargeType
    {
        Null,
        Cartridge_7_62,
        Cartridge_5_45
    }

    [Serializable]
    internal struct ChargeTypeComponent
    {
        public ChargeType Type { get; }
    }
}