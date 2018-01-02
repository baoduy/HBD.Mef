using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

namespace HBD.Mef.Mvc.Services
{
    public sealed class MefApiDependencyResolver : IDependencyResolver
    {
        private readonly IServiceLocator _serviceLocator;

        public MefApiDependencyResolver(IServiceLocator serviceLocator) => _serviceLocator = serviceLocator;

        public IDependencyScope BeginScope() => this;

        public void Dispose()
        {
            //Ignore
        }

        public object GetService(Type serviceType) => _serviceLocator.GetInstance(serviceType);

        public IEnumerable<object> GetServices(Type serviceType) => _serviceLocator.GetAllInstances(serviceType);
    }
}