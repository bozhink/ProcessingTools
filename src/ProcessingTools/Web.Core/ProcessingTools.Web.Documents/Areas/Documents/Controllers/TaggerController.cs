﻿// <copyright file="TaggerController.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Areas.Documents.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Constants;
    using ProcessingTools.Services.Contracts.Documents;
    using ProcessingTools.Web.Documents.Constants;
    using ProcessingTools.Web.Models.Documents.Publishers;
    using ProcessingTools.Web.Services.Contracts.Documents;

    /// <summary>
    /// /Documents/Tagger
    /// </summary>
    [Authorize]
    [Area(AreaNames.Documents)]
    public class TaggerController : Controller
    {
        private readonly IDocumentProcessingService service;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaggerController"/> class.
        /// </summary>
        /// <param name="service">Service</param>
        /// <param name="logger">Logger</param>
        public TaggerController(IDocumentProcessingService service, ILogger<TaggerController> logger)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// /Documents/Tagger
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        public IActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// /Documents/Tagger/ParseReferences
        /// </summary>
        /// <param name="documentId">Document object ID.</param>
        /// <param name="articleId">Article object ID.</param>
        /// <returns><see cref="IActionResult"/></returns>
        public async Task<IActionResult> ParseReferences(string documentId, string articleId)
        {
            try
            {
                var result = await this.service.ParseReferencesAsync(documentId, articleId).ConfigureAwait(false);
                this.logger.LogInformation("{0}", result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "TaggerController.ParseReferences");
            }

            return this.RedirectToAction(ArticlesController.DocumentsActionName, ArticlesController.ControllerName, new { id = articleId });
        }
    }
}
