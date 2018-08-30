// <copyright file="JsonToClassesController.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Areas.Tools.Controllers
{
    using System;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
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

        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonToClassesController"/> class.
        /// </summary>
        /// <param name="logger">Logger</param>
        public JsonToClassesController(ILogger<JsonToClassesController> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// GET Encode
        /// </summary>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(IndexActionName)]
        public IActionResult Index(string returnUrl)
        {
            const string LogMessage = "GET JsonToClasses/Index";

            this.logger.LogTrace(LogMessage);

            return this.RedirectToAction(ActionNames.Index, ControllerNames.Home, new { returnUrl });
        }

        /// <summary>
        /// JSON-to-C#
        /// </summary>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(JsonToCSharpActionName)]
        public IActionResult JsonToCSharp(string returnUrl)
        {
            const string LogMessage = "JsonToClasses/JsonToCSharp";

            this.logger.LogTrace(LogMessage);

            this.ViewData[ContextKeys.ReturnUrl] = returnUrl;

            return this.View();
        }
    }
}
