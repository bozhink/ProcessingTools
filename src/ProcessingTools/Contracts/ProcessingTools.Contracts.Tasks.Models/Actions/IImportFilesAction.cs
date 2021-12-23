// <copyright file="IImportFilesAction.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Tasks.Models.Actions
{
    /// <summary>
    /// Import files action.
    /// </summary>
    public interface IImportFilesAction
    {
        /// <summary>
        /// Gets or sets the source directory of files to be imported.
        /// </summary>
        string SourceDirectory { get; set; }

        /// <summary>
        /// Gets or sets the wildcard to filter the files to be imported in the source directory.
        /// </summary>
        string FilterWildcard { get; set; }
    }
}
