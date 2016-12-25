namespace ProcessingTools.Web.Documents.Areas.Data.Controllers
{
    using System.Net;
    using System.Web.Mvc;
    using ProcessingTools.Web.Documents.Extensions;

    [Authorize]
    public class BioController : Controller
    {
        // GET: Data/Bio
        [HttpGet]
        public ActionResult Index()
        {
            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View();
        }

        // GET: Data/Bio/Help
        [HttpGet]
        public ActionResult Help()
        {
            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View();
        }

        protected override void HandleUnknownAction(string actionName)
        {
            this.IvalidActionErrorView(actionName)
                .ExecuteResult(this.ControllerContext);
        }
    }
}
