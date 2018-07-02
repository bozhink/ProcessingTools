// <copyright file="ISystemFileMetadata.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.IO
{
    /// <summary>
    /// System file metadata.
    /// </summary>
    public interface ISystemFileMetadata
    {
        /// <summary>
        /// Gets the system file name.
        /// </summary>
        string SystemFileName { get; }
    }
}
