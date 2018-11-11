// <copyright file="RedisKeyValuePairsRepository{T}.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Common.Redis.Repositories
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Data.Common.Redis.Contracts;
    using ServiceStack.Redis;

    /// <summary>
    /// Redis key-value pairs repository.
    /// </summary>
    /// <typeparam name="T">Type of the entity.</typeparam>
    public class RedisKeyValuePairsRepository<T> : RedisSavableRepository, IRedisKeyValuePairsRepository<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RedisKeyValuePairsRepository{T}"/> class.
        /// </summary>
        /// <param name="client">Redis client to be used.</param>
        public RedisKeyValuePairsRepository(IRedisClient client)
            : base(client)
        {
        }

        /// <inheritdoc/>
        public virtual Task<object> AddAsync(string key, T value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Task.Run<object>(() =>
            {
                if (this.Client.ContainsKey(key))
                {
                    throw new KeyExistsException();
                }

                return this.Client.Add(key, value);
            });
        }

        /// <inheritdoc/>
        public virtual Task<T> GetAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Task.Run(() =>
            {
                if (!this.Client.ContainsKey(key))
                {
                    throw new KeyNotFoundException();
                }

                return this.Client.Get<T>(key);
            });
        }

        /// <inheritdoc/>
        public virtual Task<object> RemoveAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Task.Run<object>(() =>
            {
                if (!this.Client.ContainsKey(key))
                {
                    return true;
                }

                return this.Client.Remove(key);
            });
        }

        /// <inheritdoc/>
        public virtual Task<object> UpdateAsync(string key, T value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Task.Run<object>(() =>
            {
                if (!this.Client.ContainsKey(key))
                {
                    throw new KeyNotFoundException();
                }

                return this.Client.Replace(key, value);
            });
        }

        /// <inheritdoc/>
        public virtual Task<object> UpsertAsync(string key, T value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Task.Run<object>(() =>
            {
                return this.Client.Set(key, value);
            });
        }
    }
}
