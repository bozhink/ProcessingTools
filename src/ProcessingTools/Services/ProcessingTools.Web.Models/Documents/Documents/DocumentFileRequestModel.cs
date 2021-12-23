﻿// <copyright file="DocumentFileRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Documents
{
    using System.IO;
    using ProcessingTools.Contracts.Services.Models.Documents.Documents;

    /// <summary>
    /// Document file request model.
    /// </summary>
    public class DocumentFileRequestModel : IDocumentFileModel
    {
        /// <inheritdoc/>
        public string ContentType { get; set; }

        /// <inheritdoc/>
        public long ContentLength { get; set; }

        /// <inheritdoc/>
        public string FileExtension => Path.GetExtension(this.FileName);

        /// <inheritdoc/>
        public string FileName { get; set; }
    }
}
