namespace ProcessingTools.Data.Common.Memory
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using Contracts;

    public class MemoryKeyValueDataStore<TKey, TValue> : IMemoryKeyValueDataStore<TKey, TValue>
    {
        public IEnumerable<TKey> Keys => DataStore.Instance.Keys;

        public TValue this[TKey key]
        {
            get
            {
                TValue value;
                DataStore.Instance.TryGetValue(key, out value);

                return value;
            }
        }

        public TValue AddOrUpdate(TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory) => DataStore.Instance.AddOrUpdate(key: key, addValueFactory: addValueFactory, updateValueFactory: updateValueFactory);

        public bool Remove(TKey key)
        {
            TValue value;
            return DataStore.Instance.TryRemove(key, out value);
        }

        private sealed class DataStore
        {
            private static volatile ConcurrentDictionary<TKey, TValue> instance;

            private DataStore()
            {
            }

            public static ConcurrentDictionary<TKey, TValue> Instance
            {
                get
                {
                    if (instance == null)
                    {
                        var syncLock = new object();
                        lock (syncLock)
                        {
                            if (instance == null)
                            {
                                instance = new ConcurrentDictionary<TKey, TValue>();
                            }
                        }
                    }

                    return instance;
                }
            }
        }
    }
}
