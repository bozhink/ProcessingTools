// <copyright file="DocumentsController.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Api.Tagger.Controllers
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using ProcessingTools.Contracts.Services.Documents;

    /// <summary>
    /// Documents controller.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentsDataService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentsController"/> class.
        /// </summary>
        /// <param name="service">Documents service.</param>
        public DocumentsController(IDocumentsDataService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        /// <summary>
        /// Get document content by document object ID.
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <returns>The content of the document as string.</returns>
        public async Task<object> GetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return null;
            }

            string content = await this.service.GetDocumentContentAsync(id).ConfigureAwait(false);

            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return new { content };
        }
    }
}
