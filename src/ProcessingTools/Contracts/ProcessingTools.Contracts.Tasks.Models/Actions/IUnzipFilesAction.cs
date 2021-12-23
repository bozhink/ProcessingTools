// <copyright file="IUnzipFilesAction.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Tasks.Models.Actions
{
    /// <summary>
    /// Unzip files action.
    /// </summary>
    public interface IUnzipFilesAction : IFileOverwritable
    {
        /// <summary>
        /// Gets or sets the source directory of archive files to be decompressed.
        /// </summary>
        string SourceDirectory { get; set; }

        /// <summary>
        /// Gets or sets the wildcard to filter the archive files to be decompressed in the source directory.
        /// </summary>
        string FilterWildcard { get; set; }

        /// <summary>
        /// Gets or sets the destination directory where archive files have to be decompressed.
        /// </summary>
        string DestinationDirectory { get; set; }
    }
}
