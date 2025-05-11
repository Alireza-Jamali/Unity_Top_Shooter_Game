using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    /// <summary>
    /// Pub/Sub system with generic event support
    /// Observer Pattern
    /// </summary>
    public static class EventSystem
    {
        private static readonly Dictionary<Type, List<Delegate>> _subscribers = new();
        private static readonly object _lock = new();

        public static void Subscribe<T>(Action<T> handler)
        {
            lock (_lock)
            {
                var eventType = typeof(T);
                if (!_subscribers.ContainsKey(eventType))
                {
                    _subscribers[eventType] = new List<Delegate>();
                }
                _subscribers[eventType].Add(handler);
            }
        }

        /// <summary>
        /// Unsubscribe from event type T
        /// </summary>
        /// <typeparam name="T">Event type</typeparam>
        /// <param name="handler">Original callback method</param>
        public static void Unsubscribe<T>(Action<T> handler)
        {
            if (handler == null) return;

            lock (_lock)
            {
                var eventType = typeof(T);
                if (!_subscribers.TryGetValue(eventType, out var handlers)) return;

                // Remove all matching handlers
                handlers.RemoveAll(h => h.Equals(handler));

                // Clean up empty lists
                if (handlers.Count == 0)
                {
                    _subscribers.Remove(eventType);
                }
            }
        }
        
        public static void Publish<T>(T eventData)
        {
            List<Delegate> handlers;
            lock (_lock)
            {
                if (!_subscribers.TryGetValue(typeof(T), out handlers)) return;
            }

            foreach (var handler in handlers.Cast<Action<T>>().Where(handler => handler != null))
            {
                handler.Invoke(eventData);
            }
        }

        public static void Clear() => _subscribers.Clear();
    }
}