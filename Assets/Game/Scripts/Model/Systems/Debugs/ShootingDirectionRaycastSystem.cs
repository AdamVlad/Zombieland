﻿using Assets.Game.Scripts.Model.Components;
using Assets.Plugins.IvaLeoEcsLite.Extensions;
using Assets.Plugins.IvaLeoEcsLite.UnityEcsComponents;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Game.Scripts.Model.Systems.Debugs
{
    internal sealed class ShootingDirectionRaycastSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<PlayerTagComponent,
                MonoLink<Transform>,
                ShootingComponent>> _filter = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var transform = ref _filter.Get2(entity).Value;
                ref var shootingComponent = ref _filter.Get3(entity);

                if (!shootingComponent.IsShooting) return;

                Debug.DrawRay(transform.position, shootingComponent.Direction * 10, Color.red);
            }
        }
    }
}