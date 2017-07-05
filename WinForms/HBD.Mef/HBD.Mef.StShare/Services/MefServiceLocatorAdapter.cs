#if NETSTANDARD2_0
using System.Composition;
using Microsoft.Practices.ServiceLocation;
using System.Collections.Generic;
using System;
using System.Linq;

namespace HBD.Mef.Services
{
    public class MefServiceLocatorAdapter : ServiceLocatorImplBase
    {
        private readonly CompositionContext _compositionContainer;

        public MefServiceLocatorAdapter(CompositionContext compositionContainer)
        {
            _compositionContainer = compositionContainer;
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
            => _compositionContainer.GetExports(serviceType, string.Empty);

        protected override object DoGetInstance(Type serviceType, string key)
        {
            var exports = _compositionContainer.GetExports(serviceType, key);
            return exports.FirstOrDefault();
        }
    }
}
#endif