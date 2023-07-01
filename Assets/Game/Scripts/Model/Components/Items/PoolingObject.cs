using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Game.Scripts.Model.Components.Items
{
    internal class PoolingObject : MonoBehaviour
    {
        public IObjectPool<PoolingObject> Pool { get; set; }

        public void ReturnToPool()
        {
            Pool.Release(this);
        }
    }
}