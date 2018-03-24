// <copyright file="Document.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Data.Documents
{
    using System;
    using ProcessingTools.Models.Contracts.Services.Data.Documents;

    /// <summary>
    /// Document service model.
    /// </summary>
    public class Document : IDocument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Document"/> class.
        /// </summary>
        public Document()
        {
            this.Id = Guid.NewGuid().ToString();
            this.DateCreated = DateTime.UtcNow;
            this.DateModified = this.DateCreated;
        }

        /// <inheritdoc/>
        public string Id { get; set; }

        /// <inheritdoc/>
        public string Comment { get; set; }

        /// <inheritdoc/>
        public long ContentLength { get; set; }

        /// <inheritdoc/>
        public string ContentType { get; set; }

        /// <inheritdoc/>
        public DateTime DateCreated { get; set; }

        /// <inheritdoc/>
        public DateTime DateModified { get; set; }

        /// <inheritdoc/>
        public string FileName { get; set; }

        /// <inheritdoc/>
        public string FileExtension { get; set; }
    }
}
