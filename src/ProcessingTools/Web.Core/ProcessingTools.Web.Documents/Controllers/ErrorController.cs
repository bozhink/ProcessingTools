// <copyright file="ErrorController.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using ProcessingTools.Web.Documents.Constants;
    using ProcessingTools.Web.Documents.Models;

    /// <summary>
    /// Error
    /// </summary>
    public class ErrorController : Controller
    {
        /// <summary>
        /// Controller Name
        /// </summary>
        public const string ControllerName = "Error";

        /// <summary>
        /// Index Action Name
        /// </summary>
        public const string IndexActionName = nameof(Index);

        /// <summary>
        /// Code Action Name
        /// </summary>
        public const string CodeActionName = nameof(Code);

        /// <summary>
        /// Handle Unknown Action Action Name
        /// </summary>
        public const string HandleUnknownActionActionName = nameof(HandleUnknownAction);

        /// <summary>
        /// /Error
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [ActionName(IndexActionName)]
        public IActionResult Index()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// /Error/Code/{id}
        /// </summary>
        /// <param name="id">Status code.</param>
        /// <returns><see cref="IActionResult"/></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [ActionName(CodeActionName)]
        public IActionResult Code(string id)
        {
            return this.View(model: id);
        }

        /// <summary>
        /// /Error/HandleUnknownAction
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [ActionName(HandleUnknownActionActionName)]
        public IActionResult HandleUnknownAction()
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
