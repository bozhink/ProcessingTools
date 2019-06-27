// <copyright file="IMessageCacheService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Cache
{
    /// <summary>
    /// Message cache service.
    /// </summary>
    public interface IMessageCacheService
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        string Message { get; set; }
    }
}
