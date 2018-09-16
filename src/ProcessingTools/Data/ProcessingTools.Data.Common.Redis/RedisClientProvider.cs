// <copyright file="RedisClientProvider.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Common.Redis
{
    using System;
    using ProcessingTools.Data.Common.Redis.Contracts;
    using ServiceStack.Redis;

    /// <summary>
    /// Redis client provider.
    /// </summary>
    public class RedisClientProvider : IRedisClientProvider
    {
        private readonly string connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisClientProvider"/> class.
        /// </summary>
        /// <param name="connectionString">Connection string for the Redis client.</param>
        public RedisClientProvider(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            this.connectionString = connectionString;
        }

        /// <inheritdoc/>
        public IRedisClient Create()
        {
            return new RedisClient(this.connectionString);
        }
    }
}
