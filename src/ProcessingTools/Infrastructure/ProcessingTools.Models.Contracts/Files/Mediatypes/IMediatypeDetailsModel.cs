// <copyright file="IMediatypeDetailsModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Files.Mediatypes
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
