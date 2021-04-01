﻿// <copyright file="IFtpAction.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Common.Enumerations;

namespace ProcessingTools.Contracts.Tasks.Models.Actions
{
    /// <summary>
    /// FTP action.
    /// </summary>
    public interface IFtpAction : IFileOverwritable
    {
        /// <summary>
        /// Gets or sets the FTP host address.
        /// </summary>
        string Host { get; set; }

        /// <summary>
        /// Gets or sets the FTP port.
        /// </summary>
        int Port { get; set; }

        /// <summary>
        /// Gets or sets the FTP protocol.
        /// </summary>
        FtpProtocol Protocol { get; set; }

        /// <summary>
        /// Gets or sets the FTP user name.
        /// </summary>
        string User { get; set; }

        /// <summary>
        /// Gets or sets the FTP password.
        /// </summary>
        string Password { get; set; }
    }
}
