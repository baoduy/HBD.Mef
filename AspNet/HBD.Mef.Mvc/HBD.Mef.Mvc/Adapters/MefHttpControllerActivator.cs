using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using log4net;

namespace HBD.Mef.Mvc.Adapters
{
    [Export(typeof(IHttpControllerActivator))]
    public class MefHttpControllerActivator : IHttpControllerActivator
    {
        private readonly CompositionContainer _container;

        [ImportingConstructor]
        public MefHttpControllerActivator(CompositionContainer container)
        {
            _container = container;
        }

        [Import(AllowDefault = true, AllowRecomposition = true)]
        public ILog Logger { protected get; set; }

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor,
            Type controllerType)
        {
            if (controllerType == null) return null;

            var export = _container.GetExports(controllerType, null, controllerType.FullName).FirstOrDefault() ??
                         _container.GetExports(typeof(IHttpController), null, controllerType.FullName).FirstOrDefault();

            if (export != null) return (IHttpController) export.Value;

            Logger?.Warn($"The Controller '{controllerType.FullName}' is not found in Mef.");
            return null;
        }
    }
}