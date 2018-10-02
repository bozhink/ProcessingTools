// <copyright file="MemoryKeyCollectionValuePairsRepository{T}.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Common.Memory.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Data.Common.Memory.Contracts;

    /// <summary>
    /// Memory key-collection-value pairs repository.
    /// </summary>
    /// <typeparam name="T">Type of the entity.</typeparam>
    public class MemoryKeyCollectionValuePairsRepository<T> : IMemoryKeyCollectionValuePairsRepository<T>
    {
        private readonly IMemoryStringKeyCollectionValueDataStore<T> dataStore;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryKeyCollectionValuePairsRepository{T}"/> class.
        /// </summary>
        /// <param name="dataStore">Data store.</param>
        public MemoryKeyCollectionValuePairsRepository(IMemoryStringKeyCollectionValueDataStore<T> dataStore)
        {
            this.dataStore = dataStore ?? throw new ArgumentNullException(nameof(dataStore));
        }

        /// <inheritdoc/>
        public IEnumerable<string> Keys => this.dataStore.Keys;

        /// <inheritdoc/>
        public Task<object> AddAsync(string key, T value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var result = this.dataStore.AddOrUpdate(
                key,
                k =>
                {
                    var collection = new HashSet<T>
                    {
                        value
                    };
                    return collection;
                },
                (k, l) =>
                {
                    l.Add(value);
                    return l;
                });

            return Task.FromResult<object>(result);
        }

        /// <inheritdoc/>
        public IEnumerable<T> GetAll(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            return this.dataStore[key];
        }

        /// <inheritdoc/>
        public Task<object> RemoveAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Task.FromResult<object>(this.dataStore.Remove(key));
        }

        /// <inheritdoc/>
        public Task<object> RemoveAsync(string key, T value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            this.dataStore.AddOrUpdate(
                key,
                k => new HashSet<T>(),
                (k, c) =>
                {
                    c.Remove(value);
                    return c;
                });

            return Task.FromResult<object>(true);
        }

        /// <inheritdoc/>
        public Task<object> SaveChangesAsync() => Task.FromResult<object>(0);
    }
}
