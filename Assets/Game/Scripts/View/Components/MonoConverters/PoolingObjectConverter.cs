using AB_Utility.FromSceneToEntityConverter;
using Assets.Game.Scripts.Model.Components.Items;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;

namespace Assets.Game.Scripts.View.Components.MonoConverters
{
    internal sealed class PoolingObjectConverter : ComponentConverter<MonoLink<PoolingObject>>
    {
    }
}