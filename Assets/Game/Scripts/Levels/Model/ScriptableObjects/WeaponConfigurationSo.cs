using Assets.Game.Scripts.Levels.Model.Components.Data.Charges;

using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.ScriptableObjects
{
    [CreateAssetMenu(fileName = "WeaponSettings", menuName = "Settings/WeaponSettings")]
    internal sealed class WeaponConfigurationSo : ScriptableObject
    {
        [Space]
        [SerializeField] private Sprite _icon;
        public Sprite Icon => _icon;

        [Space, Header("Clip")]

        [SerializeField] private Charge _charge;
        public Charge Charge => _charge;

        [SerializeField, Range(1, 1000)] private int _clipCapacity;
        public int ClipCapacity => _clipCapacity;

        [Space, Header("Shooting")]

        [SerializeField, Range(1, 1000)] private int _damage;
        public int Damage => _damage;

        [SerializeField, Range(0, 100)] private float _shootingDistance;
        public float ShootingDistance => _shootingDistance;

        [SerializeField, Range(1, 9)] private float _shootingPower;
        public float ShootingPower => _shootingPower;

        [SerializeField, Range(0, 5)] private float _shootingDelay;
        public float ShootingDelay => _shootingDelay;

        [SerializeField, Range(0,5)] private float _reloadingTime;
        public float ReloadingTime => _reloadingTime;
    }
}