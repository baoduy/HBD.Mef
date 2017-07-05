using System.ComponentModel.Composition.Primitives;
using System.Web.Http;
using HBD.Api.Shell.Models;
using System.ComponentModel.Composition;

namespace HBD.Api.Shell.Controllers
{
    public class AboutController : ApiController
    {
        [HttpGet]
        public AboutInfo Get()
        {
            return new AboutInfo
            {
                Title = "The HBD Workspace for WebApi",
                Version = "1.0.0",
                Description =
                    "This Workspace is building based on the Managed Extensibility Framework (Mef) and WebApi. That allows you to develop and maintain independence WebApi Modules easily."
            };
        }
    }
}