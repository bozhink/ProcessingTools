// <copyright file="IZipFilesAction.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Tasks.Models.Actions
{
    /// <summary>
    /// Zip files action.
    /// </summary>
    public interface IZipFilesAction : IFileOverwritable
    {
        /// <summary>
        /// Gets or sets the source directory of files to be archived.
        /// </summary>
        string SourceDirectory { get; set; }

        /// <summary>
        /// Gets or sets the wildcard to filter the files to be archived in the source directory.
        /// </summary>
        string FilterWildcard { get; set; }

        /// <summary>
        /// Gets or sets the destination directory where archive file have to be stored.
        /// </summary>
        string DestinationDirectory { get; set; }
    }
}
