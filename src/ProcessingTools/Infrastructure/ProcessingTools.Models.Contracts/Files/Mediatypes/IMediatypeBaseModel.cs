// <copyright file="IMediatypeBaseModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Files.Mediatypes
{
    /// <summary>
    /// Mediatype base model.
    /// </summary>
    public interface IMediatypeBaseModel : IMediatypeMetaModel
    {
        /// <summary>
        /// Gets the description of the mediatype.
        /// </summary>
        string Description { get; }
    }
}
