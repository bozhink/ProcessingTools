// <copyright file="IMediatypeMetaModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Files.Mediatypes
{
    /// <summary>
    /// Mediatype meta model.
    /// </summary>
    public interface IMediatypeMetaModel : IMediatype
    {
        /// <summary>
        /// Gets the file extension.
        /// </summary>
        string Extension { get; }
    }
}
