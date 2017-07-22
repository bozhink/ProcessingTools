namespace ProcessingTools.Web.Controllers
{
    using System;
    using System.Web.Mvc;
    using ProcessingTools.Contracts;
    using ProcessingTools.Web.Constants;

    public class ErrorController : Controller
    {
        public const string AreaName = AreaNames.DefaultArea;
        public const string ControllerName = "Error";
        public const string IndexActionName = RouteValues.IndexActionName;
        public const string LastErrorKey = "_____exception_____";

        private readonly ILogger logger;

        public ErrorController(ILogger logger)
        {
            this.logger = logger;
        }

        [HttpGet, ActionName(IndexActionName)]
        public ActionResult Index(Exception ex)
        {
            if (ex == null && this.HttpContext.Cache[LastErrorKey] != null)
            {
                ex = this.HttpContext.Cache[LastErrorKey] as Exception;
                this.HttpContext.Cache.Remove(LastErrorKey);
            }

            if (ex != null)
            {
                this.logger?.Log(ex, ControllerName);
            }

            return this.View(model: ex);
        }
    }
}
