// <copyright file="TextEditorController.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Areas.Tools.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
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

        /// <summary>
        /// Tools/TextEditor.
        /// </summary>
        /// <returns><see cref="IActionResult"/>.</returns>
        [ActionName(IndexActionName)]
        public IActionResult Index()
        {
            return this.RedirectToAction(MonacoEditorActionName);
        }

        /// <summary>
        /// Tools/TextEditor/MonacoEditor.
        /// </summary>
        /// <returns><see cref="IActionResult"/>.</returns>
        [ActionName(MonacoEditorActionName)]
        public IActionResult MonacoEditor()
        {
            return this.View();
        }

        /// <summary>
        /// Tools/TextEditor/CodeMirror.
        /// </summary>
        /// <returns><see cref="IActionResult"/>.</returns>
        [ActionName(CodeMirrorActionName)]
        public IActionResult CodeMirror()
        {
            return this.View();
        }
    }
}
