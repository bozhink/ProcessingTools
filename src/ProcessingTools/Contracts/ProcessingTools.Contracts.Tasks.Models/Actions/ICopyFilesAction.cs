// <copyright file="ICopyFilesAction.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Tasks.Models.Actions
{
    /// <summary>
    /// Copy files action.
    /// </summary>
    public interface ICopyFilesAction : IFileOverwritable
    {
        /// <summary>
        /// Gets or sets the source directory of files to be copied.
        /// </summary>
        string SourceDirectory { get; set; }

        /// <summary>
        /// Gets or sets the wildcard to filter the files to be copied in the source directory.
        /// </summary>
        string FilterWildcard { get; set; }

        /// <summary>
        /// Gets or sets the destination directory where files have to be copied.
        /// </summary>
        string DestinationDirectory { get; set; }
    }
}
