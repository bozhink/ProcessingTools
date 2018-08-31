// <copyright file="ISmsService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Web.Services
{
    using Microsoft.AspNet.Identity;

    /// <summary>
    /// Expose a way to send SMS messages
    /// </summary>
    public interface ISmsService : IIdentityMessageService
    {
    }
}
