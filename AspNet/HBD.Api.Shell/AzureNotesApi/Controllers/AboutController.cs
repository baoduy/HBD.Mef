using System.Web.Http;

namespace AzureNotesApi.Controllers
{
    public class AboutController : ApiController
    {
        [HttpGet]
        public string Get()
        {
            return "The Azure Notes WebApi. Version 1.0.0";
        }
    }
}