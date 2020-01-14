// <copyright file="MediatypeMetaModel.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Files.Mediatypes
{
    using ProcessingTools.Contracts.Models.Files.Mediatypes;

    /// <summary>
    /// Mediatype meta model.
    /// </summary>
    public class MediatypeMetaModel : IMediatypeMetaModel
    {
        /// <inheritdoc/>
        public string Extension { get; set; }

        /// <inheritdoc/>
        public string MimeType { get; set; }

        /// <inheritdoc/>
        public string MimeSubtype { get; set; }
    }
}
