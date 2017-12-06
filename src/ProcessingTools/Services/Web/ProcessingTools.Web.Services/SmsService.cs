// <copyright file="SmsService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services
{
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using ProcessingTools.Contracts.Web.Services;

    /// <summary>
    /// Default SMS sending service.
    /// </summary>
    public class SmsService : ISmsService
    {
        /// <summary>
        /// Sends a SMS message
        /// </summary>
        /// <param name="message">Message to be sent</param>
        /// <returns>Task</returns>
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
