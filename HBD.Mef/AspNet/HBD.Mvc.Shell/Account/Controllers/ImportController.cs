#region using

using System.ComponentModel.Composition;
using System.Web.Mvc;

#endregion

namespace Account.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Authorize(Roles = "Edit")]
    public class ImportController : Controller
    {
        [Authorize(Roles = "Import")]
        public ActionResult ImportFromFile()
        {
            return View();
        }

        public ActionResult ImportFromService()
        {
            return View();
        }
    }
}