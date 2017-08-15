namespace ProcessingTools.Contracts.Data.Repositories
{
    using System.Collections.Generic;

    public interface IKeyListableRepository<out TKey> : IRepository
    {
        IEnumerable<TKey> Keys { get; }
    }
}
