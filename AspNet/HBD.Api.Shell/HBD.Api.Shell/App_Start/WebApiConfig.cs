using System.Web.Http;

namespace HBD.Api.Shell
{
    public static class WebApiConfig
    {
        private const string ShellName = "Shell";

        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{area}/{controller}/{id}",
                new {area = ShellName, controller = "About", id = RouteParameter.Optional}
            );
        }
    }
}