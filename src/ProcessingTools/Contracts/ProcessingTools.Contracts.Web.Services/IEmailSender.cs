// <copyright file="IEmailSender.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

using System.Threading.Tasks;

namespace ProcessingTools.Contracts.Web.Services
{
    /// <summary>
    /// E-mail sender.
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Sends e-mail message.
        /// </summary>
        /// <param name="email">E-mail address of the recipient.</param>
        /// <param name="subject">Subject of the message.</param>
        /// <param name="message">Text content of the message.</param>
        /// <returns>Task.</returns>
        Task SendEmailAsync(string email, string subject, string message);
    }
}
