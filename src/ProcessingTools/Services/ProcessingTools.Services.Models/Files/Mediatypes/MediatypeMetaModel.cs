// <copyright file="MediatypeMetaModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Files.Mediatypes
{
    using ProcessingTools.Services.Models.Contracts.Files.Mediatypes;

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
