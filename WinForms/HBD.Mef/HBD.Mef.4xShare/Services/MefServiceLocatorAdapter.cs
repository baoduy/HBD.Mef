#region using

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using Microsoft.Practices.ServiceLocation;

#endregion

namespace HBD.Mef.Services
{
    public class MefServiceLocatorAdapter : ServiceLocatorImplBase
    {
        private readonly CompositionContainer _compositionContainer;

        public MefServiceLocatorAdapter(CompositionContainer compositionContainer)
        {
            _compositionContainer = compositionContainer;
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            var exports = _compositionContainer.GetExports(serviceType, null, string.Empty);
            return exports.Select(export => export.Value).ToList();
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            var exports = _compositionContainer.GetExports(serviceType, null, key);
            var instance = exports.FirstOrDefault()?.Value;
            if (instance == null)
                throw new ActivationException(
                    FormatActivationExceptionMessage(new CompositionException("Export not found"), serviceType, key));
            return instance;
        }
    }
}
