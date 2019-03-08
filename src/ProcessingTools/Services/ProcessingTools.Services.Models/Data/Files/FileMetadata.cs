﻿// <copyright file="FileMetadata.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Data.Files
{
    using System;
    using ProcessingTools.Models.Contracts.Files;

    /// <summary>
    /// File meta-data service model.
    /// </summary>
    public class FileMetadata : IFileMetadata
    {
        /// <inheritdoc/>
        public long ContentLength { get; set; }

        /// <inheritdoc/>
        public string ContentType { get; set; }

        /// <inheritdoc/>
        public string CreatedBy { get; set; }

        /// <inheritdoc/>
        public DateTime CreatedOn { get; set; }

        /// <inheritdoc/>
        public DateTime ModifiedOn { get; set; }

        /// <inheritdoc/>
        public string Description { get; set; }

        /// <inheritdoc/>
        public string FileExtension { get; set; }

        /// <inheritdoc/>
        public string FileName { get; set; }

        /// <inheritdoc/>
        public string FullName { get; set; }

        /// <inheritdoc/>
        public object Id { get; set; }

        /// <inheritdoc/>
        public string ModifiedBy { get; set; }
    }
}