// <copyright file="MessageCacheService.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Cache
{
    using System;
    using System.Collections.Concurrent;
    using ProcessingTools.Contracts.Services.Cache;

    /// <summary>
    /// Message cache service.
    /// </summary>
    public class MessageCacheService : IMessageCacheService
    {
        private readonly ConcurrentDictionary<string, string> messageCache = new ConcurrentDictionary<string, string>();
        private readonly ConcurrentDictionary<string, Exception> exceptionCache = new ConcurrentDictionary<string, Exception>();

        /// <inheritdoc/>
        public string Message
        {
            get => this.messageCache.TryGetValue(nameof(this.Message), out string message) ? message : null;

            set => this.messageCache.AddOrUpdate(nameof(this.Message), (k) => value, (k, v) => value);
        }

        /// <inheritdoc/>
        public Exception Exception
        {
            get => this.exceptionCache.TryGetValue(nameof(this.Exception), out var exception) ? exception : null;

            set => this.exceptionCache.AddOrUpdate(nameof(this.Exception), (k) => value, (k, v) => value);
        }
    }
}
