#region using

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using HBD.Mef.Logging;
using Microsoft.Practices.ServiceLocation;

#endregion

namespace HBD.Mef.Mvc.Services
{
    public class MefServiceLocatorAdapter : ServiceLocatorImplBase
    {
        private readonly CompositionContainer _compositionContainer;

        public MefServiceLocatorAdapter(CompositionContainer compositionContainer) : this(compositionContainer, null)
        {
        }

        public MefServiceLocatorAdapter(CompositionContainer compositionContainer, ILogger logger)
        {
            _compositionContainer = compositionContainer;
            Logger = logger;
        }

        protected ILogger Logger { get; }

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
                Logger?.Debug(
                    $"The instace '{serviceType.FullName}' with Key '{key}' is not found in CompositionContainer.");
            return instance;
        }
    }
}