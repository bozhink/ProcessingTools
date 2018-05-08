// <copyright file="DocumentUpdateRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Documents
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Models.Contracts.Documents.Documents;

    /// <summary>
    /// Document update request model.
    /// </summary>
    public class DocumentUpdateRequestModel : ProcessingTools.Services.Models.Contracts.Documents.Documents.IDocumentUpdateModel, ProcessingTools.Models.Contracts.IWebModel
    {
        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        public string Id { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        public string ArticleId { get; set; }

        /// <inheritdoc/>
        public string FileId { get; set; }

        /// <summary>
        /// Gets or sets the file request model.
        /// </summary>
        public DocumentFileRequestModel File { get; set; } = new DocumentFileRequestModel();

        /// <inheritdoc/>
        IDocumentFileModel IDocumentBaseModel.File => this.File;

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }

        /// <inheritdoc/>
        public string ReturnUrl { get; set; }
    }
}
