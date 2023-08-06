using Assets.Game.Scripts.Levels.Model.Components.Data.Weapons;
using Assets.Game.Scripts.Levels.Model.Practices.Builders.Base;
using Assets.Game.Scripts.Levels.Model.Practices.Builders.Context;

namespace Assets.Game.Scripts.Levels.Model.Practices.Builders
{
    internal class WeaponBuilder : EcsObjectsBuilder<Weapon>
    {
        public WeaponBuilder(EcsContext context) : base(context)
        {
        }

        private bool _withWeapon;
        private bool _withClip;
        private bool _withDamage;
        private bool _withAttackDelay;
        private bool _withReloadingDelay;
        private bool _withShooting;

        public WeaponBuilder WithWeapon()
        {
            _withWeapon = true;
            return this;
        }

        public WeaponBuilder WithClip()
        {
            _withClip = true;
            return this;
        }

        public WeaponBuilder WithDamage()
        {
            _withDamage = true;
            return this;
        }

        public WeaponBuilder WithAttackDelay()
        {
            _withAttackDelay = true;
            return this;
        }

        public WeaponBuilder WithReloadingDelay()
        {
            _withReloadingDelay = true;
            return this;
        }

        public WeaponBuilder WithWeaponShooting()
        {
            _withShooting = true;
            return this;
        }

        protected override void BuildInternal()
        {
            var weapon = ObjectGo.GetComponent<Weapon>();
            if (_withWeapon) Context.SetWeapon(weapon);
            if (_withClip) Context.SetWeaponClip(weapon);
            if (_withShooting) Context.SetWeaponShooting(weapon);
            if (_withDamage) Context.SetDamage(weapon.Settings.Damage);
            if (_withAttackDelay) Context.SetAttackDelay(weapon.Settings.ShootingDelay);
            if (_withReloadingDelay) Context.SetReloadingDelay(weapon.Settings.ReloadingTime);
        }

        protected override void ResetInternal()
        {
            _withWeapon = false;
            _withClip = false;
            _withShooting = false;
            _withDamage = false;
            _withAttackDelay = false;
            _withReloadingDelay = false;
        }
    }
}