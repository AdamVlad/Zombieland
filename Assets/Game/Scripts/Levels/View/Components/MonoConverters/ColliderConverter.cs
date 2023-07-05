using AB_Utility.FromSceneToEntityConverter;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.View.Components.MonoConverters
{
    internal sealed class ColliderConverter : ComponentConverter<MonoLink<Collider>>
    {
    }
}