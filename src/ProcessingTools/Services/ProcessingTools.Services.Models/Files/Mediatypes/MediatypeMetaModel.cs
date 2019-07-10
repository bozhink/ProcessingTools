// <copyright file="MediatypeMetaModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Models.Files.Mediatypes;

namespace ProcessingTools.Services.Models.Files.Mediatypes
{
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
