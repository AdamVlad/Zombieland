using Assets.Game.Scripts.Model.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Game.Scripts.Model.Systems
{
    internal sealed class ScreenInitSystem : IEcsInitSystem
    {
        private readonly EcsPoolInject<InputScreenPositionComponent> _pool = default;

        public void Init(IEcsSystems systems)
        {
            _pool.NewEntity(out _);
        }
    }
}