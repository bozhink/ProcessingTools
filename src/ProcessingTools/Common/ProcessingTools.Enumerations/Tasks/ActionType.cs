// <copyright file="ActionType.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Enumerations.Tasks
{
    /// <summary>
    /// Action type.
    /// </summary>
    public enum ActionType
    {
        /// <summary>
        /// Copy files.
        /// </summary>
        CopyFiles,

        /// <summary>
        /// Delete files.
        /// </summary>
        DeleteFiles,

        /// <summary>
        /// Move files.
        /// </summary>
        MoveFiles,

        /// <summary>
        /// Unzip files.
        /// </summary>
        UnZipFiles,

        /// <summary>
        /// Zip files.
        /// </summary>
        ZipFiles,

        /// <summary>
        /// Read email.
        /// </summary>
        ReadEmail,

        /// <summary>
        /// Send email.
        /// </summary>
        SendEmail,

        /// <summary>
        /// Download FTP files.
        /// </summary>
        DownloadFtpFiles,

        /// <summary>
        /// Upload FTP files.
        /// </summary>
        UploadFtpFiles
    }
}
