#region using

using System.ComponentModel.Composition;
using System.Web.Mvc;

#endregion

namespace Account.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ViewController : Controller
    {
        // GET: ViewAccount
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details()
        {
            return View();
        }
    }
}