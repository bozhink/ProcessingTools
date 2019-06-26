// <copyright file="BioController.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Areas.Data.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ProcessingTools.Web.Documents.Constants;

    /// <summary>
    /// Biology data controller.
    /// </summary>
    [Authorize]
    [Area(AreaNames.Data)]
    public class BioController : Controller
    {
        /// <summary>
        /// Controller name.
        /// </summary>
        public const string ControllerName = "Bio";

        /// <summary>
        /// Index action name.
        /// </summary>
        public const string IndexActionName = nameof(Index);

        /// <summary>
        /// Index.
        /// </summary>
        /// <returns><see cref="IActionResult"/>.</returns>
        [ActionName(IndexActionName)]
        public IActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// Help.
        /// </summary>
        /// <returns><see cref="IActionResult"/>.</returns>
        [ActionName(ActionNames.Help)]
        public IActionResult Help()
        {
            return this.View();
        }
    }
}
