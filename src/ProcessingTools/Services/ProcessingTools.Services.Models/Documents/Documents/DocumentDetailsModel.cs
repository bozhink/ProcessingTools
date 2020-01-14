// <copyright file="DocumentDetailsModel.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Documents.Documents
{
    using System;
    using ProcessingTools.Contracts.Models.Documents.Documents;

    /// <summary>
    /// Document details model.
    /// </summary>
    public class DocumentDetailsModel : Contracts.Services.Models.Documents.Documents.IDocumentDetailsModel
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <inheritdoc/>
        public string ArticleId { get; set; }

        /// <inheritdoc/>
        public string FileId { get; set; }

        /// <inheritdoc/>
        public IDocumentFileModel File { get; set; }

        /// <inheritdoc/>
        public long NumberOfFiles { get; set; }

        /// <inheritdoc/>
        public bool IsFinal { get; set; }

        /// <inheritdoc/>
        public string Description { get; set; }

        /// <inheritdoc/>
        public string CreatedBy { get; set; }

        /// <inheritdoc/>
        public DateTime CreatedOn { get; set; }

        /// <inheritdoc/>
        public string ModifiedBy { get; set; }

        /// <inheritdoc/>
        public DateTime ModifiedOn { get; set; }
    }
}
