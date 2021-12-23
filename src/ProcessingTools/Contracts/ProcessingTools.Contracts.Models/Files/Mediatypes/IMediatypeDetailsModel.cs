// <copyright file="IMediatypeDetailsModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Files.Mediatypes
{
    /// <summary>
    /// Mediatype details model.
    /// </summary>
    public interface IMediatypeDetailsModel : IMediatypeModel
    {
        /// <summary>
        /// Gets the content type.
        /// </summary>
        string ContentType { get; }
    }
}
