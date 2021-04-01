// <copyright file="IDeleteFilesAction.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Tasks.Models.Actions
{
    /// <summary>
    /// Delete files action.
    /// </summary>
    public interface IDeleteFilesAction
    {
        /// <summary>
        /// Gets or sets the source directory of files to be deleted.
        /// </summary>
        string SourceDirectory { get; set; }

        /// <summary>
        /// Gets or sets the wildcard to filter the files to be deleted in the source directory.
        /// </summary>
        string FilterWildcard { get; set; }
    }
}
