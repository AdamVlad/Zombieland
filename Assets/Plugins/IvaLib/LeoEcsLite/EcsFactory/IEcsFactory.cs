using Assets.Plugins.IvaLib.UnityLib.Factories;
using Leopotam.EcsLite;
using UnityEngine;

namespace Assets.Plugins.IvaLib.LeoEcsLite.EcsFactory
{
    public interface IEcsFactory<in TParam1, out TResult> : IFactory
    {
        TResult Create(
            TParam1 prefab,
            ref Vector3 position,
            Transform parent,
            EcsWorld world);
    }
}