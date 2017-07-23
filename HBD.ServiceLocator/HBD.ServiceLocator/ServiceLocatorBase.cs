using System;
using System.Collections.Generic;
using System.Linq;

namespace HBD.ServiceLocator
{
    public abstract class ServiceLocatorBase : IServiceLocator
    {
        public IEnumerable<object> GetAllInstances(Type serviceType)
            => DoGetAllInstances(serviceType);

        public IEnumerable<TService> GetAllInstances<TService>()
            => DoGetAllInstances(typeof(TService)).OfType<TService>();

        public object GetInstance(Type serviceType)
            => DoGetInstance(serviceType, null);

        public object GetInstance(Type serviceType, string key)
            => DoGetInstance(serviceType, key);

        public TService GetInstance<TService>()
            => (TService)DoGetInstance(typeof(TService), null);

        public TService GetInstance<TService>(string key)
            => (TService)DoGetInstance(typeof(TService), key);

        public object GetService(Type serviceType)
            => DoGetInstance(serviceType, null);

        protected abstract IEnumerable<object> DoGetAllInstances(Type serviceType);

        protected abstract object DoGetInstance(Type serviceType, string key);
    }
}
