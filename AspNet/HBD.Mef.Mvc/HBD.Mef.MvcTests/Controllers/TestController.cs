#region using

using System.ComponentModel.Composition;
using System.Web.Mvc;
using HBD.Mef.Logging;

#endregion

namespace HBD.Mef.MvcTests.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TestController : Controller
    {
        [ImportingConstructor]
        public TestController(ILogger logger)
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

        public ActionResult Contact()
        {
            return null;
        }
    }
}