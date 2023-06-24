using Assets.Game.Scripts.Model.Components;
using Assets.Plugins.IvaLeoEcsLite.Extensions;
using Assets.Plugins.IvaLeoEcsLite.UnityEcsComponents;
using Cinemachine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Game.Scripts.View.Systems
{
    internal sealed class VmCameraInitSystem : IEcsInitSystem
    {
        private readonly EcsFilterInject<Inc<MonoLink<CinemachineVirtualCamera>>> _cameraFilter = default;
        private readonly EcsFilterInject<
            Inc<PlayerTagComponent,
                MonoLink<Transform>>> _playerFilter = default;

        public void Init(IEcsSystems systems)
        {
            foreach (var cameraEntity in _cameraFilter.Value)
            {
                ref var cinemachineVm = ref _cameraFilter.Get1(cameraEntity).Value;

                if (!_playerFilter.GetFirstEntity(out var playerEntity)) continue;

                ref var playerTransform = ref _playerFilter.Get2(playerEntity).Value;

                cinemachineVm.Follow = playerTransform;
                cinemachineVm.LookAt = playerTransform;
            }
        }
    }
}
