// <copyright file="EmailSender.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services
{
    using System.Threading.Tasks;
    using ProcessingTools.Web.Services.Contracts;

    /// <summary>
    /// Default e-mail sender
    /// </summary>
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        /// <inheritdoc/>
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.CompletedTask;
        }
    }
}
