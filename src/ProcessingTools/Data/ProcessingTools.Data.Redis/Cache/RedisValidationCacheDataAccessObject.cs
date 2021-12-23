// <copyright file="RedisValidationCacheDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Redis.Cache
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.DataAccess.Cache;
    using ProcessingTools.Contracts.DataAccess.Models.Cache;
    using ProcessingTools.Contracts.Models.Cache;
    using ProcessingTools.Data.Models.Redis.Cache;
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
        public async Task<bool> AddAsync(string key, IValidationCacheModel model)
        {
            if (string.IsNullOrWhiteSpace(key) || model is null)
            {
                return false;
            }

            await Task.CompletedTask.ConfigureAwait(false);

            var list = this.client.Lists[key];
            list.Add(this.Serialize(new ValidationCacheEntity(model)));

            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> RemoveAsync(string key, IValidationCacheModel model)
        {
            if (string.IsNullOrWhiteSpace(key) || model is null)
            {
                return false;
            }

            await Task.CompletedTask.ConfigureAwait(false);

            var list = this.client.Lists[key];
            return list.Remove(this.Serialize(new ValidationCacheEntity(model)));
        }

        /// <inheritdoc/>
        public async Task<bool> RemoveAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return false;
            }

            await Task.CompletedTask.ConfigureAwait(false);

            return !this.client.ContainsKey(key) || this.client.Remove(key);
        }

        /// <inheritdoc/>
        public async Task<bool> ClearCacheAsync()
        {
            await Task.CompletedTask.ConfigureAwait(false);

            var keys = this.client.GetAllKeys();
            this.client.RemoveAll(keys);
            return true;
        }

        /// <inheritdoc/>
        public async Task<IList<IValidationCacheDataTransferObject>> GetAllForKeyAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return Array.Empty<IValidationCacheDataTransferObject>();
            }

            await Task.CompletedTask.ConfigureAwait(false);

            var list = this.client.Lists[key];
            return list.Select(this.Deserialize).ToArray<IValidationCacheDataTransferObject>();
        }

        /// <inheritdoc/>
        public async Task<IValidationCacheDataTransferObject> GetLastForKeyAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return null;
            }

            await Task.CompletedTask.ConfigureAwait(false);

            var list = this.client.Lists[key];
            return list.Select(this.Deserialize).OrderByDescending(i => i.LastUpdate).FirstOrDefault<IValidationCacheDataTransferObject>();
        }
    }
}
