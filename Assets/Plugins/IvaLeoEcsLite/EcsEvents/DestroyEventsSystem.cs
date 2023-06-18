using System;
using System.Collections.Generic;
using Leopotam.EcsLite;

namespace Assets.Plugins.IvaLeoEcsLite.EcsEvents
{
    public class DestroyEventsSystem : IEcsRunSystem
    {
        private readonly EventsBus eventsBus;
        private readonly List<Action> destructionActions;

        public DestroyEventsSystem(EventsBus eventsBus, int capacity)
        {
            this.eventsBus = eventsBus;
            destructionActions = new List<Action>(capacity);
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var action in destructionActions)
            {
                action();
            }
        }

        public DestroyEventsSystem IncReplicant<R>() where R : struct, IEventReplicant
        {
            destructionActions.Add(() => eventsBus.DestroyEvents<R>());
            return this;
        }

        public DestroyEventsSystem IncSingleton<S>() where S : struct, IEventSingleton
        {
            destructionActions.Add(() => eventsBus.DestroyEventSingleton<S>());
            return this;
        }
    }
}