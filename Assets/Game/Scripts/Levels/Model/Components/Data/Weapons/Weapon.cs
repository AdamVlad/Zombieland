using Assets.Game.Scripts.Levels.Model.ScriptableObjects;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents.EntityReference;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Components.Data.Weapons
{
    [RequireComponent(
        typeof(BoxCollider),
        typeof(Rigidbody),
        typeof(EntityReference))]
    internal class Weapon : MonoBehaviour 
    {
        [SerializeField] private WeaponConfigurationSo _settings;
        public WeaponConfigurationSo Settings => _settings;

        [SerializeField] private Transform _shootPoint;
        public Transform ShootPoint => _shootPoint;
    }
}