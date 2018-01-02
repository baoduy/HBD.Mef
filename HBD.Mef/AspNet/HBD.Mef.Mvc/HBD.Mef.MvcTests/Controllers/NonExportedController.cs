#region using

using System.ComponentModel.Composition;
using System.Web.Mvc;
using HBD.Mef.Logging;

#endregion

namespace HBD.Mef.MvcTests.Controllers
{
    [Authorize(Roles = "Admin,Support")]
    public class NonExportedController : Controller
    {
        [ImportingConstructor]
        public NonExportedController(ILogger logger)
        {
            Logger = logger;
        }

        public ILogger Logger { get; }

        public ActionResult Index()
        {
            return null;
        }

        public ActionResult About()
        {
            return null;
        }

        [Authorize(Roles = "View")]
        public ActionResult Contact()
        {
            return null;
        }
    }
}