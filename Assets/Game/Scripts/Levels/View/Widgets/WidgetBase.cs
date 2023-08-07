using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;

using Leopotam.EcsLite;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.View.Widgets
{
    internal interface IStatView<in TValue>
    {
        public void OnInit(TValue value, EcsWorld world);
        public void OnUpdate(TValue value, EcsWorld world);
    }

    internal abstract class WidgetBase : MonoBehaviour
    {
    }

    internal static class WidgetExtensions
    {
        public static void BindWidget<TWidget>(this TWidget widget, EcsWorld world, int entity) where TWidget : WidgetBase
        {
            world.Add<MonoLink<TWidget>>(entity) = new MonoLink<TWidget> { Value = widget };
        }
    }
}