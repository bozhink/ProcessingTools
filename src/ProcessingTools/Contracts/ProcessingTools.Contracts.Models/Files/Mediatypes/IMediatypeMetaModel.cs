﻿// <copyright file="IMediatypeMetaModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Files.Mediatypes
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
