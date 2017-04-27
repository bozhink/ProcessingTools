namespace ProcessingTools.Contracts.Data.Repositories
{
    using System.Collections.Generic;

    public interface IIterableRepository<T> : IRepository<T>
    {
        IEnumerable<T> Entities { get; }
    }
}
