using System;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Components.Data
{
    [Serializable]
    internal struct BackpackComponent
    {
        public Transform WeaponHolderPoint;

        [HideInInspector] public int WeaponEntity;

        [HideInInspector] public bool IsWeaponInHand => WeaponEntity != -1;
    }
}