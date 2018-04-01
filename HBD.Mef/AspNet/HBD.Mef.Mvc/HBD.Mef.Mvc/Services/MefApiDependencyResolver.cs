#region

using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

#endregion

namespace HBD.Mef.Mvc.Services
{
    public sealed class MefApiDependencyResolver : IDependencyResolver
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
            //Ignore
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