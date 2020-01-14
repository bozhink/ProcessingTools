// <copyright file="IMediatype.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Files.Mediatypes
{
    /// <summary>
    /// Mediatype.
    /// </summary>
    public interface IMediatype
    {
        /// <summary>
        /// Gets MIME type.
        /// </summary>
        string MimeType { get; }

        /// <summary>
        /// Gets the MIME subtype.
        /// </summary>
        string MimeSubtype { get; }
    }
}
