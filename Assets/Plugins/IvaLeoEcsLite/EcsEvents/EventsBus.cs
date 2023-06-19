using Leopotam.EcsLite;
using System;
using System.Collections.Generic;

namespace Assets.Plugins.IvaLeoEcsLite.EcsEvents
{
    public class EventsBus
    {
        private readonly EcsWorld _eventsWorld;
        private readonly Dictionary<Type, int> _singletonEntities;
        private readonly Dictionary<Type, EcsFilter> _cachedFilters;

        public EventsBus(int capacityEvents = 8, int capacityEventsSingleton = 8)
        {
            _eventsWorld = new EcsWorld();
            _singletonEntities = new Dictionary<Type, int>(capacityEventsSingleton);
            _cachedFilters = new Dictionary<Type, EcsFilter>(capacityEvents);
        }

        #region EventsSingleton

        public ref T NewEventSingleton<T>() where T : struct, IEventSingleton
        {
            var type = typeof(T);
            var eventsPool = _eventsWorld.GetPool<T>();
            if (!_singletonEntities.TryGetValue(type, out var eventEntity))
            {
                eventEntity = _eventsWorld.NewEntity();
                _singletonEntities.Add(type, eventEntity);
                return ref eventsPool.Add(eventEntity);
            }

            return ref eventsPool.Get(eventEntity);
        }

        public bool HasEventSingleton<T>() where T : struct, IEventSingleton
        {
            return _singletonEntities.ContainsKey(typeof(T));
        }

        public bool HasEventSingleton<T>(out T eventBody) where T : struct, IEventSingleton
        {
            var hasEvent = _singletonEntities.TryGetValue(typeof(T), out var eventEntity);
            eventBody = hasEvent ? _eventsWorld.GetPool<T>().Get(eventEntity) : default;
            return hasEvent;
        }

        public ref T GetEventBodySingleton<T>() where T : struct, IEventSingleton
        {
            var eventEntity = _singletonEntities[typeof(T)];
            var eventsPool = _eventsWorld.GetPool<T>();
            return ref eventsPool.Get(eventEntity);
        }

        public void DestroyEventSingleton<T>() where T : struct, IEventSingleton
        {
            var type = typeof(T);
            if (_singletonEntities.TryGetValue(type, out var eventEntity))
            {
                _eventsWorld.DelEntity(eventEntity);
                _singletonEntities.Remove(type);
            }
        }

        #endregion

        #region Events

        public ref T NewEvent<T>() where T : struct, IEventReplicant
        {
            var newEntity = _eventsWorld.NewEntity();
            return ref _eventsWorld.GetPool<T>().Add(newEntity);
        }

        private EcsFilter GetFilter<T>() where T : struct, IEventReplicant
        {
            var type = typeof(T);
            if (!_cachedFilters.TryGetValue(type, out var filter))
            {
                filter = _eventsWorld.Filter<T>().End();
                _cachedFilters.Add(type, filter);
            }

            return filter;
        }

        public EcsFilter GetEventBodies<T>(out EcsPool<T> pool) where T : struct, IEventReplicant
        {
            pool = _eventsWorld.GetPool<T>();
            return GetFilter<T>();
        }

        public bool HasEvents<T>() where T : struct, IEventReplicant
        {
            var filter = GetFilter<T>();
            return filter.GetEntitiesCount() != 0;
        }

        public void DestroyEvents<T>() where T : struct, IEventReplicant
        {
            foreach (var eventEntity in GetFilter<T>())
            {
                _eventsWorld.DelEntity(eventEntity);
            }
        }

        #endregion

        public EcsWorld GetEventsWorld()
        {
            return _eventsWorld;
        }

        public void Destroy()
        {
            _singletonEntities.Clear();
            _cachedFilters.Clear();
            _eventsWorld.Destroy();
        }
    }
}
