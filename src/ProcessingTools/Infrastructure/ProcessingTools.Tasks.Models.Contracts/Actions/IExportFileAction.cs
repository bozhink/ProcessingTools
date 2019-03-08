// <copyright file="IExportFileAction.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Tasks.Models.Contracts.Actions
{
    /// <summary>
    /// Export file action.
    /// </summary>
    public interface IExportFileAction : IFileOverwritable
    {
        /// <summary>
        /// Gets or sets the destination directory where the file have to be exported.
        /// </summary>
        string DestinationDirectory { get; set; }

        /// <summary>
        /// Gets or sets the name of the exported file.
        /// </summary>
        string FileName { get; set; }
    }
}
