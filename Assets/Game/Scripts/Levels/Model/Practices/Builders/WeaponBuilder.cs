using System;

using Assets.Game.Scripts.Levels.Model.Components.Data.Charges;
using Assets.Game.Scripts.Levels.Model.Components.Data.Weapons;
using Assets.Game.Scripts.Levels.Model.Practices.Builders.Base;
using Assets.Game.Scripts.Levels.Model.Practices.Builders.Context;

using UnityEngine.Pool;

namespace Assets.Game.Scripts.Levels.Model.Practices.Builders
{
    internal class WeaponBuilder : EcsObjectsBuilder<RangedWeapon>
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
        private bool _withConnectToPlayer;

        private IObjectPool<Charge> _chargesPool;

        public WeaponBuilder WithWeapon()
        {
            _withWeapon = true;
            return this;
        }

        public WeaponBuilder WithClip(IObjectPool<Charge> chargesPool)
        {
            _chargesPool = chargesPool ?? throw new ArgumentNullException();
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

        public WeaponBuilder ConnectToPlayer()
        {
            _withConnectToPlayer = true;
            return this;
        }

        protected override void BuildInternal()
        {
            var weapon = ObjectGo.GetComponent<RangedWeapon>();

            if (_withWeapon) Context.SetWeapon(weapon);
            if (_withClip) Context.SetWeaponClip(weapon.ClipCapacity, _chargesPool);
            if (_withShooting) Context.SetWeaponShooting(weapon.ShootPoint, weapon.ShootingDistance, weapon.ShootingPower);
            if (_withDamage) Context.SetDamage(weapon.Damage);
            if (_withAttackDelay) Context.SetAttackDelay(weapon.Cooldown);
            if (_withReloadingDelay) Context.SetReloadingDelay(weapon.ReloadingTime);
            if (_withConnectToPlayer) Context.ConnectToPlayer();
        }

        protected override void ResetInternal()
        {
            _withWeapon = false;
            _withClip = false;
            _withShooting = false;
            _withDamage = false;
            _withAttackDelay = false;
            _withReloadingDelay = false;
            _withConnectToPlayer = false;

            _chargesPool = null;
        }
    }
}