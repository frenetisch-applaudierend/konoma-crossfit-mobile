﻿
using System;
using System.Collections.Generic;

namespace Konoma.CrossFit
{
    public class PlatformServiceRegistry
    {
        public delegate T ServiceProvider<T>();
        private delegate object AnyServiceProvider();

        #region State

        private readonly ISet<Type> InstantiatingEphemeralServices = new HashSet<Type>();
        private readonly IDictionary<Type, object> InstantiatedStaticServices = new Dictionary<Type, object>();
        private readonly IDictionary<Type, AnyServiceProvider> RegisteredEphemeralServices = new Dictionary<Type, AnyServiceProvider>();
        private readonly IDictionary<Type, AnyServiceProvider> RegisteredStaticServices = new Dictionary<Type, AnyServiceProvider>();

        #endregion

        #region Registration

        public void RegisterService<T>(ServiceProvider<T> provider)
        {
            this.CheckAlreadyRegistered(typeof(T));

            this.RegisteredStaticServices[typeof(T)] = () => provider();
        }

        public void RegisterEphemeralService<T>(ServiceProvider<T> provider)
        {
            this.CheckAlreadyRegistered(typeof(T));

            this.RegisteredEphemeralServices[typeof(T)] = () => provider();
        }

        private void CheckAlreadyRegistered(Type type)
        {
            if (this.InstantiatedStaticServices.ContainsKey(type) || this.RegisteredStaticServices.ContainsKey(type) || this.RegisteredEphemeralServices.ContainsKey(type))
            {
                throw new InvalidOperationException($"Service {type} was already registered");
            }
        }

        #endregion

        #region Instantiation

        public T GetService<T>()
        {
            return (T)(
                this.GetInstantiatedService(typeof(T)) ??
                this.InstantiateStaticService(typeof(T)) ??
                this.InstantiateEphemeralService(typeof(T)) ??
                throw new InvalidOperationException($"Service {typeof(T)} was not registered")
            );
        }

        private object GetInstantiatedService(Type type)
        {
            return this.InstantiatedStaticServices.TryGetValue(type, out object service) ? service : null;
        }

        private object InstantiateStaticService(Type type)
        {
            if (this.InstantiatedStaticServices.ContainsKey(type))
            {
                throw new InvalidOperationException($"There is a cyclic dependency when instantiating {type}");
            }

            if (!this.RegisteredStaticServices.ContainsKey(type))
            {
                return null;
            }

            this.InstantiatedStaticServices[type] = null; // mark as being instantiated
            var service = this.RegisteredStaticServices[type].Invoke();
            this.RegisteredStaticServices.Remove(type);
            this.InstantiatedStaticServices[type] = service;

            return service;
        }

        private object InstantiateEphemeralService(Type type)
        {
            if (this.InstantiatingEphemeralServices.Contains(type))
            {
                throw new InvalidOperationException($"There is a cyclic dependency when instantiating {type}");
            }

            if (!this.RegisteredEphemeralServices.ContainsKey(type))
            {
                return null;
            }

            try
            {
                this.InstantiatingEphemeralServices.Add(type);
                return this.RegisteredEphemeralServices[type].Invoke();
            }
            finally
            {
                this.InstantiatingEphemeralServices.Remove(type);
            }
        }

        #endregion
    }
}
