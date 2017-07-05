using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Microsoft.Practices.ServiceLocation;

namespace HBD.Mef.Mvc.Services
{
    public class MefApiDependencyResolver : IDependencyResolver
    {
        private readonly IServiceLocator _serviceLocator;

        public MefApiDependencyResolver(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public void Dispose()
        {
        }

        public object GetService(Type serviceType)
        {
            return _serviceLocator.GetInstance(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _serviceLocator.GetAllInstances(serviceType);
        }
    }
}