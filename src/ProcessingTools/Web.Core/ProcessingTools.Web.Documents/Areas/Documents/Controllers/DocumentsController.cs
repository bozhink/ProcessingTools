// <copyright file="DocumentsController.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Areas.Documents.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Constants;
    using ProcessingTools.Web.Documents.Constants;
    using ProcessingTools.Web.Models.Documents.Articles;
    using ProcessingTools.Web.Services.Contracts.Documents;

    /// <summary>
    /// /Documents/Documents
    /// </summary>
    public class DocumentsController : Controller
    {
        /// <summary>
        /// Controller name.
        /// </summary>
        public const string ControllerName = "Documents";

        /// <summary>
        /// Index action name.
        /// </summary>
        public const string IndexActionName = nameof(Index);

        /// <summary>
        /// Edit action name.
        /// </summary>
        public const string EditActionName = nameof(Edit);

        /// <summary>
        /// Delete action name.
        /// </summary>
        public const string DeleteActionName = nameof(Delete);

        /// <summary>
        /// Details action name.
        /// </summary>
        public const string DetailsActionName = nameof(Details);

        /// <summary>
        /// /Documents/Documents/Index
        /// </summary>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(IndexActionName)]
        public IActionResult Index(string returnUrl = null)
        {
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                return this.Redirect(returnUrl);
            }

            return this.RedirectToAction(ArticlesController.IndexActionName, ArticlesController.ControllerName, new { returnUrl });
        }

        /// <summary>
        /// /Documents/Documents/Index
        /// </summary>
        /// <param name="articleId">ID of the article</param>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(IndexActionName)]
        public IActionResult Index(string articleId, string returnUrl = null)
        {
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                return this.Redirect(returnUrl);
            }

            return this.RedirectToAction(ArticlesController.DocumentsActionName, ArticlesController.ControllerName, new { id = articleId, returnUrl });
        }

        public IActionResult Edit()
        {
            return this.View();
        }

        public IActionResult Delete()
        {
            return this.View();
        }

        public IActionResult Details()
        {
            return this.View();
        }

    }
}
