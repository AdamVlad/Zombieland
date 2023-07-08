using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Game.Scripts.Levels.Model.Components.Weapons.Charges
{
    [RequireComponent(
        typeof(Rigidbody))]
    internal class Bullet : MonoBehaviour
    {
        public ChargeType Type;

        public void Construct(IObjectPool<Bullet> pool)
        {
            _pool = pool ?? throw new ArgumentNullException();
        }

        private void OnDestroy()
        {
            _pool?.Release(this);
        }

        private IObjectPool<Bullet> _pool;
    }
}