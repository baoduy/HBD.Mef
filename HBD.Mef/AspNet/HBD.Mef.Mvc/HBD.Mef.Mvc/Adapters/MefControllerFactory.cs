#region

using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using log4net;

#endregion

namespace HBD.Mef.Mvc.Adapters
{
    [Export(typeof(IControllerFactory))]
    public class MefControllerFactory : DefaultControllerFactory
    {
        private readonly CompositionContainer _container;

        [ImportingConstructor]
        public MefControllerFactory(CompositionContainer container)
        {
            _container = container;
        }

        [Import(AllowDefault = true, AllowRecomposition = true)]
        public ILog Logger { protected get; set; }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null) return base.GetControllerInstance(requestContext, null);

            var export = _container.GetExports(controllerType, null, controllerType.FullName).FirstOrDefault() ??
                         _container.GetExports(typeof(IController), null, controllerType.FullName).FirstOrDefault();

            if (export != null) return (IController) export.Value;

            Logger?.Warn($"The Controller '{controllerType.FullName}' is not found in Mef.");
            return base.GetControllerInstance(requestContext, controllerType);
        }

        public override void ReleaseController(IController controller)
        {
            base.ReleaseController(controller);
            ((IDisposable) controller).Dispose();
        }
    }
}