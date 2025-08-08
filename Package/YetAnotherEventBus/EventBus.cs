using System;
using System.Collections.Generic;
using UnityEngine;

namespace YetAnotherEventBus
{
    public static class EventBus
    {
        private static Dictionary<Type, List<Delegate>> _handlers;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize() =>
            _handlers = new Dictionary<Type, List<Delegate>>();

        public static void Register<TEvent>(Action<TEvent> handler) where TEvent : struct, IEvent
        {
            Type eventType = typeof(TEvent);
            
            if (!_handlers.ContainsKey(eventType))
                _handlers[eventType] = new List<Delegate>();
            
            _handlers[eventType].Add(handler);
        }

        public static void Deregister<TEvent>(Action<TEvent> handler) where TEvent : struct, IEvent
        {
            Type eventType = typeof(TEvent);
            
            if (_handlers.TryGetValue(eventType, out List<Delegate> handlers))
            {
                if (handlers.Remove(handler))
                {
                    // Free the key references for GC collection
                    if (handlers.Count == 0)
                    {
                        _handlers.Remove(eventType);
                    }
                }
            }
        }

        public static void Raise<TEvent>(TEvent @event) where TEvent : struct, IEvent
        {
            if (!_handlers.TryGetValue(typeof(TEvent), out List<Delegate> handlers))
                return;
            
            foreach (Delegate handler in handlers)
                ((Action<TEvent>)handler).Invoke(@event);
        }
    }
}


