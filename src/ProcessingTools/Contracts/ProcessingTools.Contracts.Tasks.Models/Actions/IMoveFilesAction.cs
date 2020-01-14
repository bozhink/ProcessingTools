// <copyright file="IMoveFilesAction.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Tasks.Models.Actions
{
    /// <summary>
    /// Move files action.
    /// </summary>
    public interface IMoveFilesAction : IFileOverwritable
    {
        /// <summary>
        /// Gets or sets the source directory of files to be moved.
        /// </summary>
        string SourceDirectory { get; set; }

        /// <summary>
        /// Gets or sets the wildcard to filter the files to be moved in the source directory.
        /// </summary>
        string FilterWildcard { get; set; }

        /// <summary>
        /// Gets or sets the destination directory where files have to be moved.
        /// </summary>
        string DestinationDirectory { get; set; }
    }
}
