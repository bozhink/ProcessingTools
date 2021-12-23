// <copyright file="IReadEmailAction.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Tasks.Models.Actions
{
    /// <summary>
    /// Read e-mail action.
    /// </summary>
    public interface IReadEmailAction : IEmailAction, IFileOverwritable
    {
        /// <summary>
        /// Gets or sets the subject of the e-mail to be filtered.
        /// </summary>
        string Subject { get; set; }

        /// <summary>
        /// Gets or sets the destination directory where attachments have to be stored.
        /// </summary>
        string DestinationDirectory { get; set; }
    }
}
