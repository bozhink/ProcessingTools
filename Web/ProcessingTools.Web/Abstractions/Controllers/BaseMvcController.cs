namespace ProcessingTools.Web.Abstractions.Controllers
{
    using System.Net;
    using System.Web.Mvc;

    public abstract class BaseMvcController : Controller
    {
        public const string IndexActionName = "Index";
        public const string HelpActionName = "Help";

        [HttpGet, ActionName(IndexActionName)]
        public virtual ActionResult Index()
        {
            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View();
        }

        [HttpGet, ActionName(HelpActionName)]
        public virtual ActionResult Help()
        {
            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View();
        }
    }
}
