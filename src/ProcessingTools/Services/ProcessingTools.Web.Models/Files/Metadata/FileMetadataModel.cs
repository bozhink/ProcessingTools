// <copyright file="FileMetadataModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Files.Metadata
{
    using System;
    using ProcessingTools.Contracts.Models.Files;

    /// <summary>
    /// File meta-data model.
    /// </summary>
    public class FileMetadataModel : IFileMetadata
    {
        /// <summary>
        /// Gets or sets the ID of the file.
        /// </summary>
        public object Id { get; set; }

        /// <summary>
        /// Gets or sets the file name.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the file extension.
        /// </summary>
        public string FileExtension { get; set; }

        /// <summary>
        /// Gets or sets the file full name.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the content length of the file.
        /// </summary>
        public long ContentLength { get; set; }

        /// <summary>
        /// Gets or sets the content-type of the file.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the file description.
        /// </summary>
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
