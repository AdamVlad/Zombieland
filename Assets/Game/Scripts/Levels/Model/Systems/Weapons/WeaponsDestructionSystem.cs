﻿using Assets.Game.Scripts.Levels.Model.Components.Delayed;
using Assets.Game.Scripts.Levels.Model.Components.Weapons;
using Assets.Game.Scripts.Levels.Model.Services;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Systems.Weapons
{
    internal sealed class WeaponsDestructionSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<DestructionDelayed,
                MonoLink<Weapon>>> _filter = default;

        private readonly EcsPoolInject<DestructionDelayed> _destructionPool = default;
        private readonly EcsPoolInject<MonoLink<Transform>> _transformPool = default;

        private readonly EcsCustomInject<WeaponsProviderService> _weaponsService = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                if (_transformPool.Has(entity))
                {
                    ref var transform = ref _transformPool.Get(entity).Value;
                    _weaponsService.Value.Return(transform.gameObject);
                }

                _destructionPool.Del(entity);
            }
        }
    }
}