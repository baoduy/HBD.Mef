#if NETSTANDARD2_0 || NETSTANDARD1_6
using System.Composition;
using System.Collections.Generic;
using System;
using System.Linq;

namespace HBD
{
    public class MefServiceLocator : ServiceLocatorBase
    {
        private readonly CompositionContext _compositionContainer;

        public MefServiceLocator(CompositionContext compositionContainer)
            => _compositionContainer = compositionContainer ?? throw new ArgumentNullException(nameof(compositionContainer));

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
            => _compositionContainer.GetExports(serviceType, string.Empty);

        protected override object DoGetInstance(Type serviceType, string key)
            => _compositionContainer.GetExport(serviceType, key);
    }
}
#endif