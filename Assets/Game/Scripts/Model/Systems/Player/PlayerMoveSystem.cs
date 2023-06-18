using Assets.Game.Scripts.Model.Components;
using Assets.Plugins.IvaLeoEcsLite.UnityEcsComponents;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Game.Scripts.Model.Systems.Player
{
    internal sealed class PlayerMoveSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MonoLink<Transform>, InputMoveComponent, MoveComponent>> _filter = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var pools = _filter.Pools;

                ref var transform = ref pools.Inc1.Get(entity).Value;
                ref var inputMoveComponent = ref pools.Inc2.Get(entity);
                ref var moveSpeed = ref pools.Inc3.Get(entity).Speed;

                //transform.position += new Vector3(moveInput.x * moveSpeed, 0, moveInput.y * moveSpeed);

                if (!inputMoveComponent.IsMoveInputStarted) continue;

                transform.position += transform.forward * moveSpeed;
            }
        }
    }
}
