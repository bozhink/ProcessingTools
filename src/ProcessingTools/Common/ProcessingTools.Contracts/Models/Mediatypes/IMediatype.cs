// <copyright file="IMediatype.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Mediatypes
{
    /// <summary>
    /// Media type.
    /// </summary>
    public interface IMediatype
    {
        /// <summary>
        /// Gets mime sub-type.
        /// </summary>
        string Mimesubtype { get; }

        /// <summary>
        /// Gets mime type.
        /// </summary>
        string Mimetype { get; }
    }
}
