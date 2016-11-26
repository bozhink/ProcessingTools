namespace ProcessingTools.Data.Common.Memory.Contracts
{
    using System;
    using System.Collections.Generic;

    public interface IMemoryKeyValueDataStore<TKey, TValue>
    {
        IEnumerable<TKey> Keys { get; }

        TValue this[TKey key] { get; }

        TValue AddOrUpdate(TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory);

        bool Remove(TKey key);
    }
}
