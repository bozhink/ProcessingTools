﻿// <copyright file="ActionTypes.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Enumerations
{
    /// <summary>
    /// Action types.
    /// </summary>
    public enum ActionTypes
    {
        /// <summary>
        /// Copy files.
        /// </summary>
        CopyFiles = 110,

        /// <summary>
        /// Delete files.
        /// </summary>
        DeleteFiles = 120,

        /// <summary>
        /// Move files.
        /// </summary>
        MoveFiles = 130,

        /// <summary>
        /// Unzip files.
        /// </summary>
        UnzipFiles = 210,

        /// <summary>
        /// Zip files.
        /// </summary>
        ZipFiles = 220,

        /// <summary>
        /// Read e-mail.
        /// </summary>
        ReadEmail = 310,

        /// <summary>
        /// Send e-mail.
        /// </summary>
        SendEmail = 320,

        /// <summary>
        /// FTP download files.
        /// </summary>
        FtpDownloadFiles = 410,

        /// <summary>
        /// FTP upload files.
        /// </summary>
        FtpUploadFiles = 420
    }
}
