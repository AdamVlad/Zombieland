using System;
using UnityEngine;

namespace Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents
{
    [Serializable]
    public struct MonoLink<TComponent> where TComponent : Component
    {
        public TComponent Value;
    }
}
