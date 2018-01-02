using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using HBD.Mef.Mvc.Core;
using HBD.Mef.Logging;

namespace HBD.Mef.Mvc.Adapters
{
    /// <summary>
    ///     This MefApiHttpControllerSelector allow to load the duplicated controllers from difference binaries based on the
    ///     Area name in the router.
    ///     This selector had been tested on the route "api/{area}/{controller}/{id}".
    ///     If the Area on the request is not found in the Module Mapping. The default controller in Shell binary will be
    ///     picked up.
    /// </summary>
    public class MefApiHttpControllerSelector : DefaultHttpControllerSelector
    {
        private const string AreaRouteName = "area";
        private const string ControllerRouteName = "controller";

        public MefApiHttpControllerSelector(HttpConfiguration configuration) : base(configuration)
        {
            Configuration = configuration;
        }

        public MefApiHttpControllerSelector(HttpConfiguration configuration, ILogger logger) : base(configuration)
        {
            Configuration = configuration;
            Logger = logger;
        }

        protected HttpConfiguration Configuration { get; }
        protected ILogger Logger { get; }

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            var routeData = request.GetRouteData();
            if (routeData == null)
                return base.SelectController(request);

            var areaName = routeData.GetRouteVariable<string>(AreaRouteName);
            var controllerName = routeData.GetRouteVariable<string>(ControllerRouteName);

            Logger?.Debug($"SelectController area: '{areaName}' and controller: '{controllerName}'");

            if (areaName == null || controllerName == null)
                return base.SelectController(request);

            var assembliesResolver =
                Configuration.Services.GetAssembliesResolver();
            var controllersResolver =
                Configuration.Services.GetHttpControllerTypeResolver();

            var controllerTypes =
                controllersResolver.GetControllerTypes(assembliesResolver)
                    .Where(t => t.Name.StartsWith(controllerName, StringComparison.OrdinalIgnoreCase)).ToArray();

            Type[] foundTypes = null;
            if (controllerTypes.Length > 0)
            {
                var areaMap = ServiceLocator.Current.GetAllInstances<IMefApiRegistration>()
                    .ToDictionary(a => a.AreaName.ToLower(), a => a.GetType().Assembly.FullName);

                if (areaMap.ContainsKey(areaName.ToLower()))
                {
                    var assName = areaMap[areaName.ToLower()];
                    foundTypes = controllerTypes.Where(c => c.Assembly.FullName == assName).ToArray();
                }
                else
                {
                    foundTypes = controllerTypes.Where(c => !areaMap.Values.Contains(c.Assembly.FullName))
                        .ToArray();
                }
            }

            //If there is more than 1 type in the Assembly then the exception will be threw in the base class.
            if (foundTypes == null || foundTypes.Length <= 0)
            {
                Logger?.Exception($"The area '{areaName}' or controller: '{controllerName}' is not found.");
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }


            Logger?.Debug($"SelectController Type found: '{foundTypes[0].FullName}'");
            return new HttpControllerDescriptor(Configuration, controllerName, foundTypes[0]);
        }
    }
}