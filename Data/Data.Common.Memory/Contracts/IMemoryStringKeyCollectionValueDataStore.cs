namespace ProcessingTools.Data.Common.Memory.Contracts
{
    using System.Collections.Generic;

    public interface IMemoryStringKeyCollectionValueDataStore<T> : IMemoryKeyValueDataStore<string, ICollection<T>>
    {
    }
}
