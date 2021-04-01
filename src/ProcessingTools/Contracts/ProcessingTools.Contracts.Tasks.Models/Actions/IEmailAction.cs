// <copyright file="IEmailAction.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Tasks.Models.Actions
{
    /// <summary>
    /// E-mail action.
    /// </summary>
    public interface IEmailAction
    {
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
    }
}
