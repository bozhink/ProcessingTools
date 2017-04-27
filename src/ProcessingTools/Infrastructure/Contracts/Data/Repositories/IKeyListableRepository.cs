namespace ProcessingTools.Contracts.Data.Repositories
{
    using System.Collections.Generic;

    public interface IKeyListableRepository<TKey> : IRepository
    {
        IEnumerable<TKey> Keys { get; }
    }
}
