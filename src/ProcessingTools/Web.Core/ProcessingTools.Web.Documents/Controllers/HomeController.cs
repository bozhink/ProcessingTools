// <copyright file="HomeController.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using ProcessingTools.Web.Documents.Constants;
    using ProcessingTools.Web.Documents.Models;

    /// <summary>
    /// Home
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Controller Name
        /// </summary>
        public const string ControllerName = "Home";

        /// <summary>
        /// Index Action Name
        /// </summary>
        public const string IndexActionName = nameof(Index);

        /// <summary>
        /// About Action Name
        /// </summary>
        public const string AboutActionName = nameof(About);

        /// <summary>
        /// Contact Action Name
        /// </summary>
        public const string ContactActionName = nameof(Contact);

        /// <summary>
        /// Error Action Name
        /// </summary>
        public const string ErrorActionName = nameof(Error);

        /// <summary>
        /// Index
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(IndexActionName)]
        public IActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// About
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(AboutActionName)]
        public IActionResult About()
        {
            this.ViewData[ContextKeys.Message] = "Your application description page.";

            return this.View();
        }

        /// <summary>
        /// Contact
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(ContactActionName)]
        public IActionResult Contact()
        {
            this.ViewData[ContextKeys.Message] = "Your contact page.";

            return this.View();
        }

        /// <summary>
        /// Error
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(ErrorActionName)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
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
