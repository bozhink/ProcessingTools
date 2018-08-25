// <copyright file="HomeController.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Areas.Tools.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ProcessingTools.Web.Documents.Constants;

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
