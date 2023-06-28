﻿using Assets.Game.Scripts.Model.Components;
using Assets.Plugins.IvaLib.LeoEcsLite.Extensions;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Game.Scripts.Model.Systems.Debugs
{
    internal sealed class PlayerRotationRaycastSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<PlayerTagComponent,
                MonoLink<Transform>>> _filter = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var transform = ref _filter.Get2(entity).Value;

                Debug.DrawRay(transform.position, transform.forward * 10, Color.blue);
            }
        }
    }
}