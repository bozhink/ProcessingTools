namespace ProcessingTools.Web.Controllers
{
    using System.Web.Mvc;
    using ProcessingTools.Web.Abstractions.Controllers;
    using ProcessingTools.Web.Constants;

    [RequireHttps]
    public class HomeController : BaseMvcController
    {
        public const string ControllerName = "Home";
        public const string IndexActionName = RouteValues.IndexActionName;
        public const string AboutActionName = nameof(HomeController.About);
        public const string ContactActionName = nameof(HomeController.Contact);

        [HttpGet, ActionName(IndexActionName)]
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpGet, ActionName(AboutActionName)]
        public ActionResult About()
        {
            return this.View();
        }

        [HttpGet, ActionName(ContactActionName)]
        public ActionResult Contact()
        {
            return this.View();
        }
    }
}
