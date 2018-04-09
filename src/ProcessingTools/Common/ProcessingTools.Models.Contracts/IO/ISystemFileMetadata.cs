// <copyright file="ISystemFileMetadata.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.IO
{
    /// <summary>
    /// System file metadata.
    /// </summary>
    internal interface ISystemFileMetadata
    {
        /// <summary>
        /// Gets the system file name.
        /// </summary>
        string SystemFileName { get; }
    }
}
