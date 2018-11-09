// <copyright file="IMediatypeMetaModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Files.Mediatypes
{
    /// <summary>
    /// Mediatype meta model.
    /// </summary>
    public interface IMediatypeMetaModel
    {
        /// <summary>
        /// Gets the file extension.
        /// </summary>
        string Extension { get; }

        /// <summary>
        /// Gets the MIME type.
        /// </summary>
        string MimeType { get; }

        /// <summary>
        /// Gets the MIME subtype.
        /// </summary>
        string MimeSubtype { get; }

        /// <summary>
        /// Gets the description of the mediatype.
        /// </summary>
        string Description { get; }
    }
}
