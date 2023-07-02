using System;
using UnityEngine;

namespace Assets.Game.Scripts.Model.Components
{
    [Serializable]
    internal struct WeaponSpawnerComponent
    {
        public Transform SpawnPoint;
        [HideInInspector] public int SpawnedWeaponEntity;
    }
}