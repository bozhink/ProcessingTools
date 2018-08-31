// <copyright file="IEmailService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Web.Services
{
    using Microsoft.AspNet.Identity;

    /// <summary>
    /// Expose a way to send email messages
    /// </summary>
    public interface IEmailService : IIdentityMessageService
    {
    }
}
