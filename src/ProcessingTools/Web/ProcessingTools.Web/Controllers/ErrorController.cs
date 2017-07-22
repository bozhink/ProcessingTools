namespace ProcessingTools.Web.Controllers
{
    using System;
    using System.Web.Mvc;
    using ProcessingTools.Web.Constants;

    public class ErrorController : Controller
    {
        public const string ControllerName = "Error";
        public const string AreaName = AreaNames.DefaultArea;
        public const string IndexActionName = RouteValues.IndexActionName;

        [HttpGet, ActionName(IndexActionName)]
        public ActionResult Index(Exception model)
        {
            return this.View(model: model);
        }
    }
}
