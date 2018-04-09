// <copyright file="DocumentUpdateRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Documents
{
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using ProcessingTools.Models.Contracts.Documents.Documents;

    /// <summary>
    /// Document update request model.
    /// </summary>
    public class DocumentUpdateRequestModel : ProcessingTools.Services.Models.Contracts.Documents.Documents.IDocumentUpdateModel, IDocumentFileModel, ProcessingTools.Models.Contracts.IWebModel
    {
        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        public string Id { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        public string ArticleId { get; set; }

        /// <inheritdoc/>
        public string FileId { get; set; }

        /// <inheritdoc/>
        public string ContentType { get; set; }

        /// <inheritdoc/>
        public long ContentLength { get; set; }

        /// <inheritdoc/>
        public string FileExtension => Path.GetExtension(this.FileName);

        /// <inheritdoc/>
        public string FileName { get; set; }

        /// <inheritdoc/>
        public IDocumentFileModel File => this;

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }

        /// <inheritdoc/>
        public string ReturnUrl { get; set; }
    }
}
