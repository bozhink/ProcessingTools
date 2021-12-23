// <copyright file="ISystemFileMetadata.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.IO
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
