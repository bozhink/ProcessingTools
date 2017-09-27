// <copyright file="IMediatype.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Mediatypes.Models
{
    /// <summary>
    /// Media-type.
    /// </summary>
    public interface IMediatype
    {
        /// <summary>
        /// Gets description.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets file extension.
        /// </summary>
        string FileExtension { get; }

        /// <summary>
        /// Gets mime sub-type.
        /// </summary>
        string Mimesubtype { get; }

        /// <summary>
        /// Gets mime-type.
        /// </summary>
        string Mimetype { get; }
    }
}
