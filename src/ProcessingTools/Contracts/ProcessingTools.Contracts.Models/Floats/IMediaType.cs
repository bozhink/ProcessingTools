// <copyright file="IMediaType.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Floats
{
    /// <summary>
    /// Media-type.
    /// </summary>
    public interface IMediaType
    {
        /// <summary>
        /// Gets file extension.
        /// </summary>
        string FileExtension { get; }

        /// <summary>
        /// Gets mime-subtype.
        /// </summary>
        string MimeSubtype { get; }

        /// <summary>
        /// Gets mime-type.
        /// </summary>
        string MimeType { get; }
    }
}
