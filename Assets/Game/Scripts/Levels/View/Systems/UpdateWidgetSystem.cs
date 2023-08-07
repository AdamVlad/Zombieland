using Assets.Game.Scripts.Levels.Model.Components.Data.Requests;
using Assets.Game.Scripts.Levels.View.Widgets;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;

using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.View.Systems
{
    internal sealed class UpdateWidgetSystem<TWidget, TValue> : IEcsRunSystem where TWidget : MonoBehaviour, IStatView<TValue>
    {
        private EcsFilterInject<Inc<MonoLink<TWidget>, UpdateWidgetRequest<TWidget, TValue>>> _widgetsToUpdate = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _widgetsToUpdate.Value)
            {
                ref var pools = ref _widgetsToUpdate.Pools;

                ref var widget = ref pools.Inc1.Get(entity).Value;
                ref var request = ref pools.Inc2.Get(entity);

                widget.OnUpdate(request.Value, systems.GetWorld());
            }
        }
    }
}