#region using

using System.ComponentModel.Composition;
using System.Web.Mvc;

#endregion

namespace Customer.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ViewController : Controller
    {
        // GET: ViewCustomer
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