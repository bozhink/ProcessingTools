﻿// <copyright file="QRCodesGeneratorController.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Areas.Tools.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ProcessingTools.Web.Documents.Constants;

    /// <summary>
    /// QRCodes Generator.
    /// </summary>
    [Authorize]
    [Area(AreaNames.Tools)]
    public class QRCodesGeneratorController : Controller
    {
        /// <summary>
        /// Controller name.
        /// </summary>
        public const string ControllerName = "QRCodesGenerator";

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
