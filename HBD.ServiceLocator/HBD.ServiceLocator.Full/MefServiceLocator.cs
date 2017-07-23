#region using

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;

#endregion

namespace HBD.ServiceLocator
{
    public class MefServiceLocator : ServiceLocatorBase
    {
        private readonly CompositionContainer _compositionContainer;

        public MefServiceLocator(CompositionContainer compositionContainer) 
            => _compositionContainer = compositionContainer ?? throw new ArgumentException(nameof(compositionContainer));

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            var exports = _compositionContainer.GetExports(serviceType, null, string.Empty);
            return exports.Select(export => export.Value);
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            var exports = _compositionContainer.GetExports(serviceType, null, key);
            return exports.FirstOrDefault()?.Value;
        }
    }
}
