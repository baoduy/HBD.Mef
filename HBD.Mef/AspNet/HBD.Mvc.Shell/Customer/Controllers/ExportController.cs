#region using

using System.ComponentModel.Composition;
using System.Web.Mvc;

#endregion

namespace Customer.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ExportController : Controller
    {
        // GET: Export
        public ActionResult ExportToExcel()
        {
            return View();
        }

        public ActionResult ExportToJson()
        {
            return View();
        }
    }
}