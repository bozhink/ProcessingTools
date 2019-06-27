﻿// <copyright file="MessageCacheService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Cache
{
    using System.Collections.Concurrent;
    using ProcessingTools.Services.Contracts.Cache;

    /// <summary>
    /// Message cache service.
    /// </summary>
    public class MessageCacheService : IMessageCacheService
    {
        private readonly ConcurrentDictionary<string, string> cache = new ConcurrentDictionary<string, string>();

        /// <inheritdoc/>
        public string Message
        {
            get => this.cache.TryGetValue(nameof(this.Message), out string message) ? message : null;

            set => this.cache.AddOrUpdate(nameof(this.Message), (k) => value, (k, v) => value);
        }
    }
}
