#region using

using System.ComponentModel.Composition;
using System.Web.Mvc;

#endregion

namespace Customer.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ImportController : Controller
    {
        // GET: Import
        [Authorize(Roles = "Edit")]
        public ActionResult ImportFromFile()
        {
            return View();
        }

        [Authorize(Roles = "Edit")]
        public ActionResult ImportFromService()
        {
            return View();
        }
    }
}