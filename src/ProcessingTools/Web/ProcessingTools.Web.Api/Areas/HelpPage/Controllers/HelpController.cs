namespace ProcessingTools.Web.Api.Areas.HelpPage.Controllers
{
    using System.Web.Http;
    using System.Web.Mvc;
    using ProcessingTools.Web.Api.Areas.HelpPage.ModelDescriptions;
    using ProcessingTools.Web.Api.Areas.HelpPage.Models;

    /// <summary>
    /// The controller that will handle requests for the help page.
    /// </summary>
    public class HelpController : Controller
    {
        private const string ErrorViewName = "Error";

        public HelpController()
            : this(GlobalConfiguration.Configuration)
        {
        }

        public HelpController(HttpConfiguration config)
        {
            this.Configuration = config;
        }

        public HttpConfiguration Configuration { get; private set; }

        public ActionResult Index()
        {
            this.ViewBag.DocumentationProvider = this.Configuration.Services.GetDocumentationProvider();
            return this.View(this.Configuration.Services.GetApiExplorer().ApiDescriptions);
        }

        public ActionResult Api(string apiId)
        {
            if (!string.IsNullOrEmpty(apiId))
            {
                HelpPageApiModel apiModel = this.Configuration.GetHelpPageApiModel(apiId);
                if (apiModel != null)
                {
                    return this.View(apiModel);
                }
            }

            return this.View(ErrorViewName);
        }

        public ActionResult ResourceModel(string modelName)
        {
            if (!string.IsNullOrEmpty(modelName))
            {
                ModelDescriptionGenerator modelDescriptionGenerator = this.Configuration.GetModelDescriptionGenerator();
                if (modelDescriptionGenerator.GeneratedModels.TryGetValue(modelName, out ModelDescription modelDescription))
                {
                    return this.View(modelDescription);
                }
            }

            return this.View(ErrorViewName);
        }
    }
}