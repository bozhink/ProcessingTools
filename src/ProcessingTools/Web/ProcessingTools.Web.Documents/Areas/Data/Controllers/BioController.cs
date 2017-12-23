namespace ProcessingTools.Web.Documents.Areas.Data.Controllers
{
    using System.Web.Mvc;
    using ProcessingTools.Web.Documents.Extensions;

    [Authorize]
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

        protected override void HandleUnknownAction(string actionName)
        {
            this.IvalidActionErrorView(actionName)
                .ExecuteResult(this.ControllerContext);
        }
    }
}
