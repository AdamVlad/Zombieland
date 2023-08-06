using Assets.Game.Scripts.Levels.Model.ScriptableObjects;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsPhysics.Checkers;

using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Components.Data.Player
{
    [RequireComponent(
        typeof(OnCollisionEnterChecker),
        typeof(OnTriggerEnterChecker),
        typeof(Rigidbody))]
    internal class Player : MonoBehaviour
    {
        [SerializeField] private PlayerConfigurationSo _settings;
        public PlayerConfigurationSo Settings => _settings;

        [SerializeField] private Transform _weaponHolderPoint;
        public Transform WeaponHolderPoint => _weaponHolderPoint;

    }
}