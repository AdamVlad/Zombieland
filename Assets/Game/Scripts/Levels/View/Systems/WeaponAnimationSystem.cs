using Assets.Game.Scripts.Levels.Model.Components.Requests;
using Assets.Game.Scripts.Levels.Model.Components.Weapons;
using Assets.Game.Scripts.Levels.Model.ScriptableObjects;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;
using DG.Tweening;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.View.Systems
{
    internal sealed class WeaponAnimationSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<MonoLink<Weapon>,
                MonoLink<Transform>,
                WeaponAnimationStartRequest>> _startRequestFilter = default;

        private readonly EcsFilterInject<
            Inc<MonoLink<Weapon>,
                MonoLink<Transform>,
                WeaponAnimationStopRequest>> _stopRequestFilter = default;

        private readonly EcsCustomInject<SceneConfigurationSo> _sceneConfiguration = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _startRequestFilter.Value)
            {
                ref var transform = ref _startRequestFilter.Get2(entity).Value;
                transform
                    .DORotate(new Vector3(0, _sceneConfiguration.Value.WeaponSpawnerRotationAngle, 0),
                        _sceneConfiguration.Value.WeaponSpawnerRotationDuration,
                        RotateMode.FastBeyond360)
                    .SetLoops(-1, LoopType.Restart)
                    .SetRelative()
                    .SetEase(Ease.Linear);

                transform
                    .DOMoveY(
                        transform.position.y + 
                        _sceneConfiguration.Value.WeaponSpawnerClimbingPosition, 
                        _sceneConfiguration.Value.WeaponSpawnerClimbingDuration)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetEase(Ease.Linear);
            }

            foreach (var entity in _stopRequestFilter.Value)
            {
                ref var transform = ref _stopRequestFilter.Get2(entity).Value;
                DOTween.Kill(transform);
            }
        }
    }
}