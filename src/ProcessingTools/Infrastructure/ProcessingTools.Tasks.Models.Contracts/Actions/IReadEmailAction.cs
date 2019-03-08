// <copyright file="IReadEmailAction.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Tasks.Models.Contracts.Actions
{
    /// <summary>
    /// Read e-mail action.
    /// </summary>
    public interface IReadEmailAction : IFileOverwritable
    {
        /// <summary>
        /// Gets or sets the subject of the e-mail to be filtered.
        /// </summary>
        string Subject { get; set; }

        /// <summary>
        /// Gets or sets the host address of the mail server.
        /// </summary>
        string Host { get; set; }

        /// <summary>
        /// Gets or sets the port of the mail server.
        /// </summary>
        int Port { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use SSL connection to the mail server.
        /// </summary>
        bool UseSSL { get; set; }

        /// <summary>
        /// Gets or sets the user name for the mail server.
        /// </summary>
        string User { get; set; }

        /// <summary>
        /// Gets or sets the password for the mail server.
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// Gets or sets the destination directory where attachments have to be stored.
        /// </summary>
        string DestinationDirectory { get; set; }
    }
}
