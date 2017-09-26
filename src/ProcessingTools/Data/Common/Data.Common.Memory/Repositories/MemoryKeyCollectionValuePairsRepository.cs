namespace ProcessingTools.Data.Common.Memory.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Contracts;
    using Contracts.Repositories;

    public class MemoryKeyCollectionValuePairsRepository<T> : IMemoryKeyCollectionValuePairsRepository<T>
    {
        private readonly IMemoryStringKeyCollectionValueDataStore<T> dataStore;

        public MemoryKeyCollectionValuePairsRepository(IMemoryStringKeyCollectionValueDataStore<T> dataStore)
        {
            this.dataStore = dataStore ?? throw new ArgumentNullException(nameof(dataStore));
        }

        public IEnumerable<string> Keys => this.dataStore.Keys;

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

        public IEnumerable<T> GetAll(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            return this.dataStore[key];
        }

        public Task<object> RemoveAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Task.FromResult<object>(this.dataStore.Remove(key));
        }

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

        public object SaveChanges() => 0;

        public Task<object> SaveChangesAsync() => Task.FromResult(this.SaveChanges());
    }
}
