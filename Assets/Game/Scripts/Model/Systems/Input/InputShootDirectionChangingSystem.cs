﻿using Assets.Game.Scripts.Model.AppData;
using Assets.Game.Scripts.Model.Components;
using Assets.Plugins.IvaLeoEcsLite.Extensions;
using Assets.Plugins.IvaLeoEcsLite.UnityEcsComponents;
using Assets.Plugins.IvaLib;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Game.Scripts.Model.Systems.Input
{
    internal sealed class InputShootDirectionChangingSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                InputComponent,
                MonoLink<Transform>,
                ShootingComponent>> _shootingFilter = default;

        private readonly EcsFilterInject<Inc<InputScreenPositionComponent>> _screenFilter = default;

        private readonly EcsSharedInject<SharedData> _sharedData = default;

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
                            _sharedData.Value.MainCamera,
                            _sharedData.Value.RaycastableMask,
                            out var shootDirectionPoint
                        )) return;

                    shootingComponent.Direction = (shootDirectionPoint - transform.position).normalized;
                }
            }
        }
    }
}
