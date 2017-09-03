// <copyright file="ISmsService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Contracts.Services
{
    using Microsoft.AspNet.Identity;

    /// <summary>
    /// Expose a way to send SMS messages
    /// </summary>
    public interface ISmsService : IIdentityMessageService
    {
    }
}
