using UnityEngine;

namespace Assets.Plugins.IvaLib.UnityLib.Factories
{
    public interface IUnityFactory<in TParam1, out TResult> : IFactory
    {
         TResult Create(
             TParam1 prefab,
             ref Vector3 position,
             Transform parent);
    }
}