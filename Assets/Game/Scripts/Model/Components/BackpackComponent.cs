using System;
using UnityEngine;

namespace Assets.Game.Scripts.Model.Components
{
    [Serializable]
    internal struct BackpackComponent
    {
        public Transform WeaponHolderPoint;
        [HideInInspector] public int WeaponEntity;
    }
}