using Assets.Game.Scripts.Levels.Model.Components.Data;
using Assets.Game.Scripts.Levels.Model.Components.Data.Player;
using Assets.Game.Scripts.Levels.Model.ScriptableObjects;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;
using Assets.Plugins.IvaLib.UnityLib;

using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Systems.Input
{
    internal sealed class InputShootDirectionChangingSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject
            <Inc<InputComponent,
                MonoLink<Transform>,
                ShootingComponent>> _shootingFilter = default;

        private readonly EcsFilterInject<Inc<InputScreenPositionComponent>> _screenFilter = default;

        [Inject] private readonly SceneConfigurationSo _sceneSettings;
        [Inject] private readonly Camera _mainCamera;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _shootingFilter.Value)
            {
                ref var shootingComponent = ref _shootingFilter.Get3(entity);

                if (!shootingComponent.IsShooting) continue;

                ref var transform = ref _shootingFilter.Get2(entity).Value;

                foreach (var screenEntity in _screenFilter.Value)
                {
                    ref var screenInputPosition = ref _screenFilter.Get1(screenEntity).Position;

                    if (!ScreenPointToWorldConverter.GetWorldPointFrom(
                            ref screenInputPosition,
                            _mainCamera,
                            _sceneSettings.RaycastableMask,
                            out var shootDirectionPoint
                        )) return;

                    shootingComponent.Direction = (shootDirectionPoint - transform.position).normalized;
                }
            }
        }
    }
}
