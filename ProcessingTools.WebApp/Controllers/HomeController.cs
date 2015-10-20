namespace ProcessingTools.WebApp.Controllers
{
    using System.Web.Mvc;

    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}