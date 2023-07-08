using UnityEngine;

namespace Assets.Plugins.IvaLib.UnityLib.Factory
{
    public interface IObjectFactory<in TParam1, out TResult> : IFactory
    {
        TResult Create(
            TParam1 prefab,
            Vector3 position = default);
    }
}