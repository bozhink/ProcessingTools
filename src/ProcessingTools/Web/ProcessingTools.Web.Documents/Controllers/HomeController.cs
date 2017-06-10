namespace ProcessingTools.Web.Documents.Controllers
{
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public ActionResult About()
        {
            this.ViewBag.Message = "Your application description page.";

            return this.View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            this.ViewBag.Message = "Your contact page.";

            return this.View();
        }

        [HttpGet]
        public ActionResult Help()
        {
            return this.View();
        }
    }
}