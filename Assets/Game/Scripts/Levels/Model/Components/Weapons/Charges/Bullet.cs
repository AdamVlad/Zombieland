using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Game.Scripts.Levels.Model.Components.Weapons.Charges
{
    internal interface IBullet
    {
    }

    internal class Bullet : MonoBehaviour, IBullet
    {
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