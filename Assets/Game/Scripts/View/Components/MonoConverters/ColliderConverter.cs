using AB_Utility.FromSceneToEntityConverter;
using Assets.Plugins.IvaLeoEcsLite.UnityEcsComponents;
using UnityEngine;

namespace Assets.Game.Scripts.View.Components.MonoConverters
{
    internal sealed class ColliderConverter : ComponentConverter<MonoLink<Collider>>
    {
    }
}