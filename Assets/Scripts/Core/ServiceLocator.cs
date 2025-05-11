using System;
using System.Collections.Generic;

namespace Core
{
    /// <summary>
    /// Service Locator pattern implementation with thread safety
    /// Dependency Injection
    /// </summary>
    public static class  ServiceLocator
    {
        private static readonly Dictionary<Type, object> _services = new();
        private static readonly object _lock = new();

        public static void Register<T>(T serviceInstance)
        {
            lock (_lock)
            {
                _services[typeof(T)] = serviceInstance;
            }
        }

        public static T Get<T>() where T : class
        {
            lock (_lock)
            {
                if (_services.TryGetValue(typeof(T), out var service))
                {
                    return service as T;
                }
                throw new InvalidOperationException($"Service {typeof(T).Name} not registered");
            }
        }

        public static void Clear() => _services.Clear();
    }
}