// <copyright file="MemoryKeyValueDataStore{TKey,TValue}.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Common.Memory
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using ProcessingTools.Data.Common.Memory.Contracts;

    /// <summary>
    /// Memory key-value data store.
    /// </summary>
    /// <typeparam name="TKey">Type of the key.</typeparam>
    /// <typeparam name="TValue">Type of the value.</typeparam>
    public class MemoryKeyValueDataStore<TKey, TValue> : IMemoryKeyValueDataStore<TKey, TValue>
    {
        /// <inheritdoc/>
        public IEnumerable<TKey> Keys => DataStore.Instance.Keys;

        /// <inheritdoc/>
        public TValue this[TKey key]
        {
            get
            {
                DataStore.Instance.TryGetValue(key, out TValue value);

                return value;
            }
        }

        /// <inheritdoc/>
        public TValue AddOrUpdate(TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory) => DataStore.Instance.AddOrUpdate(key: key, addValueFactory: addValueFactory, updateValueFactory: updateValueFactory);

        /// <inheritdoc/>
        public bool Remove(TKey key)
        {
            return DataStore.Instance.TryRemove(key, out TValue value);
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
