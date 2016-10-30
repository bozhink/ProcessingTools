namespace ProcessingTools.Contracts.Data.Repositories
{
    using System.Collections.Generic;

    public interface IFullTextSearchableRepository<T> : IRepository<T>
    {
        IEnumerable<T> Search(string text);
    }
}
