// <copyright file="RedisValidationCacheDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Cache.Redis
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Data.Contracts.Cache;
    using ProcessingTools.Data.Models.Cache.Redis;
    using ProcessingTools.Data.Models.Contracts.Cache;
    using ProcessingTools.Models.Contracts.Cache;
    using ServiceStack.Redis;
    using ServiceStack.Text;

    /// <summary>
    /// Redis implementation of <see cref="IValidationCacheDataAccessObject"/>.
    /// </summary>
    public class RedisValidationCacheDataAccessObject : IValidationCacheDataAccessObject
    {
        private readonly IRedisClient client;
        private readonly IStringSerializer serializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisValidationCacheDataAccessObject"/> class.
        /// </summary>
        /// <param name="client">Instance of <see cref="IRedisClient"/>.</param>
        public RedisValidationCacheDataAccessObject(IRedisClient client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.serializer = new JsonStringSerializer();
        }

        private Func<string, ValidationCacheEntity> Deserialize => s => this.serializer.DeserializeFromString<ValidationCacheEntity>(s);

        private Func<ValidationCacheEntity, string> Serialize => e => this.serializer.SerializeToString(e);

        /// <inheritdoc/>
        public Task<object> AddAsync(string key, IValidationCacheModel model)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return Task.Run<object>(() =>
            {
                var list = this.client.Lists[key];
                list.Add(this.Serialize(new ValidationCacheEntity(model)));

                return true;
            });
        }

        /// <inheritdoc/>
        public Task<object> RemoveAsync(string key, IValidationCacheModel model)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return Task.Run<object>(() =>
            {
                var list = this.client.Lists[key];
                return list.Remove(this.Serialize(new ValidationCacheEntity(model)));
            });
        }

        /// <inheritdoc/>
        public Task<object> RemoveAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Task.Run<object>(() =>
            {
                return !this.client.ContainsKey(key) || this.client.Remove(key);
            });
        }

        /// <inheritdoc/>
        public Task<object> ClearCacheAsync()
        {
            return Task.Run<object>(() =>
            {
                var keys = this.client.GetAllKeys();
                this.client.RemoveAll(keys);
                return true;
            });
        }

        /// <inheritdoc/>
        public Task<IValidationCacheDataModel[]> GetAllForKeyAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Task.Run(() =>
            {
                var list = this.client.Lists[key];
                return list.Select(this.Deserialize).ToArray<IValidationCacheDataModel>();
            });
        }

        /// <inheritdoc/>
        public Task<IValidationCacheDataModel> GetLastForKeyAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Task.Run(() =>
            {
                var list = this.client.Lists[key];
                return list.Select(this.Deserialize).OrderByDescending(i => i.LastUpdate).FirstOrDefault<IValidationCacheDataModel>();
            });
        }
    }
}
