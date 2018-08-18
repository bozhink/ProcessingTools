// <copyright file="HomeController.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ProcessingTools.Web.Documents.Constants;

    /// <summary>
    /// Home
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Controller name.
        /// </summary>
        public const string ControllerName = "Home";

        /// <summary>
        /// Index action name.
        /// </summary>
        public const string IndexActionName = nameof(Index);

        /// <summary>
        /// About action name.
        /// </summary>
        public const string AboutActionName = nameof(About);

        /// <summary>
        /// Contact action name.
        /// </summary>
        public const string ContactActionName = nameof(Contact);

        /// <summary>
        /// Privacy action name.
        /// </summary>
        public const string PrivacyActionName = nameof(Privacy);

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
            return this.View();
        }

        /// <summary>
        /// Contact
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(ContactActionName)]
        public IActionResult Contact()
        {
            return this.View();
        }

        /// <summary>
        /// Privacy
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(PrivacyActionName)]
        public IActionResult Privacy()
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
