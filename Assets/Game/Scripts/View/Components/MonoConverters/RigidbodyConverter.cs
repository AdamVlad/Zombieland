using AB_Utility.FromSceneToEntityConverter;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;
using UnityEngine;

namespace Assets.Game.Scripts.View.Components.MonoConverters
{
    internal sealed class RigidbodyConverter : ComponentConverter<MonoLink<Rigidbody>>
    {
    }
}