// <copyright file="EmailSenderExtensions.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Services
{
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Web.Services;

    /// <summary>
    /// <see cref="IEmailSender"/> extensions.
    /// </summary>
    public static class EmailSenderExtensions
    {
        /// <summary>
        /// Sends e-mail confirmation.
        /// </summary>
        /// <param name="emailSender"><see cref="IEmailSender"/> instance.</param>
        /// <param name="email">E-mail address of the recipient.</param>
        /// <param name="link">Link to be send.</param>
        /// <returns>Task.</returns>
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(
                email,
                "Confirm your email",
                $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");
        }
    }
}
