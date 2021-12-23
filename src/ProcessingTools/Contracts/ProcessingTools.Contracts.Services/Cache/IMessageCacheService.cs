// <copyright file="IMessageCacheService.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Cache
{
    using System;

    /// <summary>
    /// Message cache service.
    /// </summary>
    public interface IMessageCacheService
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        string Message { get; set; }

        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        Exception Exception { get; set; }
    }
}
