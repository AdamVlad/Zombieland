﻿using System;
using System.Collections.Generic;
using Leopotam.EcsLite;

namespace Assets.Plugins.IvaLeoEcsLite.EcsEvents
{
    public class DestroyEventsSystem : IEcsRunSystem
    {
        private readonly EventsBus _eventsBus;
        private readonly List<Action> _destructionActions;

        public DestroyEventsSystem(EventsBus eventsBus, int capacity)
        {
            this._eventsBus = eventsBus;
            _destructionActions = new List<Action>(capacity);
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var action in _destructionActions)
            {
                action();
            }
        }

        public DestroyEventsSystem IncReplicant<R>() where R : struct, IEventReplicant
        {
            _destructionActions.Add(() => _eventsBus.DestroyEvents<R>());
            return this;
        }

        public DestroyEventsSystem IncSingleton<S>() where S : struct, IEventSingleton
        {
            _destructionActions.Add(() => _eventsBus.DestroyEventSingleton<S>());
            return this;
        }
    }
}