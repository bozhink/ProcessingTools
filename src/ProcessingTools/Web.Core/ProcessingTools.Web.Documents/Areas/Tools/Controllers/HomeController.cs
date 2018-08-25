namespace ProcessingTools.Web.Documents.Areas.Tools.Controllers
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Web.Models.Tools.Encode;
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Constants;
    using ProcessingTools.Processors.Imaging.Contracts;
    using ProcessingTools.Web.Documents.Constants;
    using ProcessingTools.Web.Models.Tools.QRCode;

    /// <summary>
    /// Home controller.
    /// </summary>
    [Authorize]
    [Area(AreaNames.Tools)]
    public class HomeController : Controller
    {
        /// <summary>
        /// Index
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(ActionNames.Index)]
        public IActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// Help
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(ActionNames.Help)]
        public IActionResult Help()
        {
            return this.View();
        }
    }
}