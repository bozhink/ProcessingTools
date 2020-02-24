// <copyright file="JsonToClassesController.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Areas.Tools.Controllers
{
    using System;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ProcessingTools.Web.Documents.Constants;

    /// <summary>
    /// JSON-to-Classes converter.
    /// </summary>
    [Authorize]
    [Area(AreaNames.Tools)]
    public class JsonToClassesController : Controller
    {
        /// <summary>
        /// Controller name.
        /// </summary>
        public const string ControllerName = "JsonToClasses";

        /// <summary>
        /// Index action name.
        /// </summary>
        public const string IndexActionName = nameof(Index);

        /// <summary>
        /// JSON-to-C# action name.
        /// </summary>
        public const string JsonToCSharpActionName = nameof(JsonToCSharp);

        /// <summary>
        /// GET Encode.
        /// </summary>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpGet]
        [ActionName(IndexActionName)]
        public IActionResult Index(Uri returnUrl)
        {
            return this.RedirectToAction(ActionNames.Index, ControllerNames.Home, new { returnUrl });
        }

        /// <summary>
        /// JSON-to-C#.
        /// </summary>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [ActionName(JsonToCSharpActionName)]
        public IActionResult JsonToCSharp(Uri returnUrl)
        {
            this.ViewData[ContextKeys.ReturnUrl] = returnUrl;

            return this.View();
        }
    }
}
