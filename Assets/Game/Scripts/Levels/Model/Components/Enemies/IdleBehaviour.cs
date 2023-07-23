﻿using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Components.Enemies
{
    internal sealed class IdleBehaviour : MonoBehaviour, IBehaviour
    {
        public float Evaluate()
        {
            return 0.1f;
        }

        public void Behave()
        {
        }
    }
}