// <copyright file="TextEditorController.cs" company="ProcessingTools">
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
    /// Text editor controller.
    /// </summary>
    [Authorize]
    [Area(AreaNames.Tools)]
    public class TextEditorController : Controller
    {
        /// <summary>
        /// Controller name.
        /// </summary>
        public const string ControllerName = "TextEditor";

        /// <summary>
        /// Index action name.
        /// </summary>
        public const string IndexActionName = nameof(Index);

        /// <summary>
        /// Monaco editor action name.
        /// </summary>
        public const string MonacoEditorActionName = nameof(MonacoEditor);

        /// <summary>
        /// CodeMirror action name.
        /// </summary>
        public const string CodeMirrorActionName = nameof(CodeMirror);

        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextEditorController"/> class.
        /// </summary>
        /// <param name="logger">Logger</param>
        public TextEditorController(ILogger<TextEditorController> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Tools/TextEditor
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(IndexActionName)]
        public IActionResult Index()
        {
            const string LogMessage = "TextEditor/Index";

            this.logger.LogTrace(LogMessage);

            return this.RedirectToAction(MonacoEditorActionName);
        }

        /// <summary>
        /// Tools/TextEditor/MonacoEditor
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(MonacoEditorActionName)]
        public IActionResult MonacoEditor()
        {
            const string LogMessage = "TextEditor/MonacoEditor";

            this.logger.LogTrace(LogMessage);

            return this.View();
        }

        /// <summary>
        /// Tools/TextEditor/CodeMirror
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(CodeMirrorActionName)]
        public IActionResult CodeMirror()
        {
            const string LogMessage = "TextEditor/CodeMirror";

            this.logger.LogTrace(LogMessage);

            return this.View();
        }
    }
}
