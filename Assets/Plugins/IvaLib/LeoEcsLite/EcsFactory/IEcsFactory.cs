using Assets.Plugins.IvaLib.UnityLib.Factory;
using Leopotam.EcsLite;
using UnityEngine;

namespace Assets.Plugins.IvaLib.LeoEcsLite.EcsFactory
{
    public interface IEcsFactory<in TParam1, out TResult> : IFactory
    {
        TResult Create(
            TParam1 prefab,
            Vector3 position,
            Transform parent,
            EcsWorld world);
    }
}