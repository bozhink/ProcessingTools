namespace ProcessingTools.Web.Controllers
{
    using System.Net;
    using System.Web.Mvc;
    using Abstractions.Controllers;

    [RequireHttps]
    public class HomeController : BaseMvcController
    {
        public const string ControllerName = "Home";
        public const string AboutActionName = "About";
        public const string ContactActionName = "Contact";

        [HttpGet, ActionName(HomeController.IndexActionName)]
        public virtual ActionResult Index()
        {
            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View();
        }

        [HttpGet, ActionName(HomeController.AboutActionName)]
        public ActionResult About()
        {
            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View();
        }

        [HttpGet, ActionName(HomeController.ContactActionName)]
        public ActionResult Contact()
        {
            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View();
        }
    }
}
