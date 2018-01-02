#region using

#endregion

using System.ComponentModel.Composition;
using System.Web.Mvc;

namespace HBD.Mvc.Shell.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}