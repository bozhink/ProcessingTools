namespace ProcessingTools.Web.Documents.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using ProcessingTools.Web.Documents.Constants;
    using ProcessingTools.Web.Documents.Models;

    public class HomeController : Controller
    {
        public const string ControllerName = "Home";
        public const string IndexActionName = nameof(Index);
        public const string AboutActionName = nameof(About);
        public const string ContactActionName = nameof(Contact);
        public const string ErrorActionName = nameof(Error);

        [ActionName(IndexActionName)]
        public IActionResult Index()
        {
            return this.View();
        }

        [ActionName(AboutActionName)]
        public IActionResult About()
        {
            this.ViewData[ContextKeys.Message] = "Your application description page.";

            return this.View();
        }

        [ActionName(ContactActionName)]
        public IActionResult Contact()
        {
            this.ViewData[ContextKeys.Message] = "Your contact page.";

            return this.View();
        }

        [ActionName(ErrorActionName)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
