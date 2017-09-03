// <copyright file="EmailService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services
{
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using ProcessingTools.Web.Contracts.Services;

    /// <summary>
    /// Default email sending service.
    /// </summary>
    public class EmailService : IEmailService
    {
        /// <summary>
        /// Sends an email message.
        /// </summary>
        /// <param name="message">Message to be sent.</param>
        /// <returns>Task</returns>
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }
}
