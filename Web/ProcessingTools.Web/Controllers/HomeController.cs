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

        [HttpGet, ActionName(AboutActionName)]
        public ActionResult About()
        {
            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View();
        }

        [HttpGet, ActionName(ContactActionName)]
        public ActionResult Contact()
        {
            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View();
        }
    }
}
