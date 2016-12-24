namespace ProcessingTools.Web.Documents.Areas.Data.Controllers
{
    using System.Web.Mvc;

    public class BioController : Controller
    {
        // GET: Data/Bio
        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }

        // GET: Data/Bio/Help
        [HttpGet]
        public ActionResult Help()
        {
            return this.View();
        }
    }
}
