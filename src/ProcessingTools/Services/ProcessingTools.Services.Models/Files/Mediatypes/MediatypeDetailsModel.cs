// <copyright file="MediatypeDetailsModel.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Files.Mediatypes
{
    using System;
    using ProcessingTools.Contracts.Models.Files.Mediatypes;

    /// <summary>
    /// Mediatype details model.
    /// </summary>
    public class MediatypeDetailsModel : IMediatypeDetailsModel
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <inheritdoc/>
        public string Extension { get; set; }

        /// <inheritdoc/>
        public string ContentType { get; set; }

        /// <inheritdoc/>
        public string MimeType { get; set; }

        /// <inheritdoc/>
        public string MimeSubtype { get; set; }

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
