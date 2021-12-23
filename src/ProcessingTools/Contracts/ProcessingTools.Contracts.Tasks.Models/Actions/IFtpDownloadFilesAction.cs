// <copyright file="IFtpDownloadFilesAction.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Tasks.Models.Actions
{
    /// <summary>
    /// FTP download files action.
    /// </summary>
    public interface IFtpDownloadFilesAction : IFtpAction
    {
        /// <summary>
        /// Gets or sets the source directory of files to be downloaded.
        /// </summary>
        string SourceDirectory { get; set; }

        /// <summary>
        /// Gets or sets the wildcard to filter the files to be downloaded in the source directory.
        /// </summary>
        string FilterWildcard { get; set; }

        /// <summary>
        /// Gets or sets the destination directory where files have to be downloaded.
        /// </summary>
        string DestinationDirectory { get; set; }
    }
}
