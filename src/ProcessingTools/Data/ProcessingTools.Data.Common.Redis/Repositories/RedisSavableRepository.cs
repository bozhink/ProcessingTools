// <copyright file="RedisSavableRepository.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Common.Redis.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Data.Contracts;
    using ServiceStack.Redis;

    /// <summary>
    /// Redis savable repository.
    /// </summary>
    public abstract class RedisSavableRepository : IKeyListableRepository<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RedisSavableRepository"/> class.
        /// </summary>
        /// <param name="client">Redis client to be used.</param>
        protected RedisSavableRepository(IRedisClient client)
        {
            this.Client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <inheritdoc/>
        public IEnumerable<string> Keys => this.Client.GetAllKeys();

        /// <summary>
        /// Gets the clent instance.
        /// </summary>
        protected IRedisClient Client { get; }

        /// <summary>
        /// Saves pending changes.
        /// </summary>
        /// <returns>Result.</returns>
        public virtual object SaveChanges()
        {
            try
            {
                this.Client.SaveAsync();
            }
            catch (RedisResponseException)
            {
                return 1;
            }

            return 0;
        }

        /// <inheritdoc/>
        public virtual Task<object> SaveChangesAsync() => Task.Run(() => this.SaveChanges());
    }
}
