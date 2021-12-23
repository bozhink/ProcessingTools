// <copyright file="ISendEmailAction.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Tasks.Models.Actions
{
    /// <summary>
    /// Send e-mail action.
    /// </summary>
    public interface ISendEmailAction : IEmailAction
    {
        /// <summary>
        /// Gets or sets the subject of the e-mail message.
        /// </summary>
        string Subject { get; set; }

        /// <summary>
        /// Gets or sets the body of the e-mail message.
        /// </summary>
        string Body { get; set; }

        /// <summary>
        /// Gets or sets the receivers of the e-mail message.
        /// </summary>
        string Receivers { get; set; }

        /// <summary>
        /// Gets or sets the sender of the e-mail message.
        /// </summary>
        string Sender { get; set; }

        /// <summary>
        /// Gets or sets the source directory of files to be attached to the e-mail message.
        /// </summary>
        string SourceDirectory { get; set; }

        /// <summary>
        /// Gets or sets the wildcard to filter the files to be attached in the source directory.
        /// </summary>
        string FilterWildcard { get; set; }
    }
}
