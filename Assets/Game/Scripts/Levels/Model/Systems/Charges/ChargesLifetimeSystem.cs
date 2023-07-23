﻿using Assets.Game.Scripts.Levels.Model.Components;
using Assets.Game.Scripts.Levels.Model.Components.Delayed;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Assets.Game.Scripts.Levels.Model.Components.Events.Charges;
using UnityEngine;
using Assets.Game.Scripts.Levels.Model.Components.Charges;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Systems.Charges
{
    internal sealed class ChargesLifetimeSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject
            <Inc<ChargeTag,
                StateComponent,
                LifetimeComponent,
                ReturnToPoolDelayed>> _filter = default;

        [Inject] private EventsBus _eventsBus;

        public void Run(IEcsSystems systems)
        {
            foreach (var chargeEntity in _filter.Value)
            {
                ref var stateComponent = ref _filter.Get2(chargeEntity);
                if (!stateComponent.IsActive) continue;

                ref var lifetimeComponent = ref _filter.Get3(chargeEntity);
                lifetimeComponent.PassedTime += Time.deltaTime;

                if (lifetimeComponent.PassedTime >= lifetimeComponent.Lifetime)
                {
                    _eventsBus.NewEvent<ChargeReturnToPoolEvent>()
                        = new ChargeReturnToPoolEvent
                        {
                            Entity = chargeEntity
                        };
                }
            }
        }
    }
}