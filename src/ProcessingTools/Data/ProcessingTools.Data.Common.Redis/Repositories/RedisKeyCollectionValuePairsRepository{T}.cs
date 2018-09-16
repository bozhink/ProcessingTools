// <copyright file="RedisKeyCollectionValuePairsRepository{T}.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Common.Redis.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Data.Common.Redis.Contracts;
    using ServiceStack.Redis;
    using ServiceStack.Text;

    /// <summary>
    /// Redis key-collection-value pairs repository.
    /// </summary>
    /// <typeparam name="T">Type of the entity.</typeparam>
    public class RedisKeyCollectionValuePairsRepository<T> : RedisSavableRepository, IRedisKeyCollectionValuePairsRepository<T>
    {
        private readonly IStringSerializer serializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisKeyCollectionValuePairsRepository{T}"/> class.
        /// </summary>
        /// <param name="client">Redis client to be used.</param>
        public RedisKeyCollectionValuePairsRepository(IRedisClient client)
            : base(client)
        {
            this.serializer = new JsonStringSerializer();
        }

        private Func<string, T> Deserialize => s => this.serializer.DeserializeFromString<T>(s);

        private Func<T, string> Serialize => e => this.serializer.SerializeToString(e);

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
                var list = this.Client.Lists[key];
                this.AddValueToList(list, value);

                return true;
            });
        }

        /// <inheritdoc/>
        public virtual IEnumerable<T> GetAll(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            var list = this.Client.Lists[key];
            return list.Select(this.Deserialize);
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
        public virtual Task<object> RemoveAsync(string key, T value)
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
                var list = this.Client.Lists[key];
                var result = this.RemoveValueFromList(list, value);

                return result;
            });
        }

        private void AddValueToList(ICollection<string> list, T value) => list.Add(this.Serialize(value));

        private bool RemoveValueFromList(ICollection<string> list, T value) => list.Remove(this.Serialize(value));
    }
}
