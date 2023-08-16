using Assets.Game.Scripts.Levels.Model.Components.Data.Charges;
using Assets.Game.Scripts.Levels.Model.ScriptableObjects;

using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Components.Data.Weapons
{
    internal class RangedWeapon : MonoBehaviour, IRangedWeapon
    {
        [SerializeField] private RangedWeaponConfigurationSo _settings;

        [SerializeField] private Transform _shootPoint;
        public Transform ShootPoint => _shootPoint;

        public Sprite Icon => _settings.Icon;
        public int Damage => _settings.Damage;
        public float Cooldown => _settings.Cooldown;
        public Charge Charge => _settings.Charge;
        public int ClipCapacity => _settings.ClipCapacity;
        public float ReloadingTime => _settings.ReloadingTime;
        public float ShootingDistance => _settings.ShootingDistance;
        public float ShootingPower => _settings.ShootingPower;
    }
}