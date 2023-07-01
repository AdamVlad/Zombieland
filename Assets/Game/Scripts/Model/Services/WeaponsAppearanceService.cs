using Assets.Game.Scripts.Model.Components.Items;
using Assets.Game.Scripts.Model.Factories;
using Assets.Game.Scripts.Model.Pools;
using Leopotam.EcsLite;
using UnityEngine;

namespace Assets.Game.Scripts.Model.Services
{
    internal class WeaponsAppearanceService : MonoBehaviour
    {
        [SerializeField] private WeaponsPool _weaponsPool;
        [SerializeField] private Transform[] _weaponsSpawnPoints;
        [SerializeField] private GameObject[] _weaponPrefab;
        [SerializeField] private Transform _spawnParent;

        public Transform[] WeaponsSpawnPoints => _weaponsSpawnPoints;

        public void Run(EcsWorld world)
        {
            var weaponFactory = new WeaponFactory();

            _weaponsPool.Construct(
                weaponFactory,
                _weaponPrefab[0],
                _weaponsSpawnPoints[0].position,
                _spawnParent,
                world,
                _weaponPrefab.Length);
        }

        public void ProvideSpawnPoint(Vector3 spawnPoint)
        {
            _weaponsPool.ProvideSpawnPoint(spawnPoint);
        }

        public PoolingObject GetWeapon()
        {
            if (_currentWeaponIndex >= _weaponPrefab.Length) return null;

            _weaponsPool.ProvidePrefab(_weaponPrefab[_currentWeaponIndex++]);

            return _weaponsPool.Pool.Get();
        }

        private int _currentWeaponIndex;
    }
}