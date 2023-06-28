using System;
using Assets.Plugins.IvaLib.UnityLib;
using Leopotam.EcsLite;

namespace Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents
{
    public class DestroyEventsSystem : IEcsRunSystem
    {
        private readonly EventsBus _eventsBus;
        private readonly FastList<Action> _destructionActions;

        public DestroyEventsSystem(EventsBus eventsBus, int capacity)
        {
            _eventsBus = eventsBus;
            _destructionActions = new FastList<Action>(capacity);
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