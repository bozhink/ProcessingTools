// <copyright file="IFtpUploadFilesAction.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Tasks.Models.Actions
{
    /// <summary>
    /// FTP upload files action.
    /// </summary>
    public interface IFtpUploadFilesAction : IFtpAction
    {
        /// <summary>
        /// Gets or sets the source directory of files to be uploaded.
        /// </summary>
        string SourceDirectory { get; set; }

        /// <summary>
        /// Gets or sets the wildcard to filter the files to be uploaded in the source directory.
        /// </summary>
        string FilterWildcard { get; set; }

        /// <summary>
        /// Gets or sets the destination directory where files have to be uploaded.
        /// </summary>
        string DestinationDirectory { get; set; }
    }
}
