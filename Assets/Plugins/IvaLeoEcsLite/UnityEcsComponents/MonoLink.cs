using System;
using UnityEngine;

namespace Assets.Plugins.IvaLeoEcsLite.UnityEcsComponents
{
    [Serializable]
    public struct MonoLink<TComponent> where TComponent : Component
    {
        public TComponent Value;
    }
}
