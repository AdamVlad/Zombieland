using UnityEngine;

namespace Assets.Plugins.IvaLib.UnityLib.Factory
{
    public interface IFactory<in TPrefab, out TResult>
    {
        TResult Create(
            TPrefab prefab,
            Vector3 position);
    }
}