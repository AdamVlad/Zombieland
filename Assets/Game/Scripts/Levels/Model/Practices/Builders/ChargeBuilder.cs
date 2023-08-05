using Assets.Game.Scripts.Levels.Model.Components.Data.Charges;
using Assets.Game.Scripts.Levels.Model.Practices.Builders.Base;
using Assets.Game.Scripts.Levels.Model.Practices.Builders.Context;

namespace Assets.Game.Scripts.Levels.Model.Practices.Builders
{
    internal class ChargeBuilder : EcsObjectsBuilder<Charge>
    {
        public ChargeBuilder(EcsContext context) : base(context)
        {
        }

        private bool _withCharge;
        private bool _withTag;
        private bool _withLifetime;
        private bool _withDamage;

        public ChargeBuilder WithCharge()
        {
            _withCharge = true;
            return this;
        }

        public ChargeBuilder WithTag()
        {
            _withTag = true;
            return this;
        }

        public ChargeBuilder WithLifetime()
        {
            _withLifetime = true;
            return this;
        }

        public ChargeBuilder WithDamage()
        {
            _withDamage = true;
            return this;
        }

        protected override void BuildInternal()
        {
            var charge = ObjectGo.GetComponent<Charge>();
            if (_withCharge) Context.SetCharge(charge);
            if (_withLifetime) Context.SetLifetime(charge.Lifetime);
            if (_withTag) Context.SetChargeTag();
            if (_withDamage) Context.SetDamage();
        }

        protected override void ResetInternal()
        {
            _withCharge = false;
            _withLifetime = false;
            _withTag = false;
            _withDamage = false;
        }
    }
}