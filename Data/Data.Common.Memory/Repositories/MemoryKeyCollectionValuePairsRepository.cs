namespace ProcessingTools.Data.Common.Memory.Repositories
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Contracts.Repositories;

    public class MemoryKeyCollectionValuePairsRepository<T> : IMemoryKeyCollectionValuePairsRepository<T>
    {
        private static readonly ConcurrentDictionary<string, ICollection<T>> DataStore = new ConcurrentDictionary<string, ICollection<T>>();

        public Task<object> Add(string key, T value)
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
                var result = DataStore.AddOrUpdate(
                    key,
                    k =>
                    {
                        var collection = new HashSet<T>();
                        collection.Add(value);
                        return collection;
                    },
                    (k, l) =>
                    {
                        l.Add(value);
                        return l;
                    });

                return result;
            });
        }

        public IEnumerable<T> GetAll(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            ICollection<T> result = null;
            DataStore.TryGetValue(key, out result);
            return result;
        }

        public Task<object> Remove(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Task.Run<object>(() =>
            {
                ICollection<T> collection = null;
                var result = DataStore.TryRemove(key, out collection);

                return result;
            });
        }

        public Task<object> Remove(string key, T value)
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
                DataStore.AddOrUpdate(
                    key,
                    k => new HashSet<T>(),
                    (k, c) =>
                    {
                        c.Remove(value);
                        return c;
                    });

                return true;
            });
        }

        public Task<long> SaveChanges() => Task.FromResult(0L);
    }
}
