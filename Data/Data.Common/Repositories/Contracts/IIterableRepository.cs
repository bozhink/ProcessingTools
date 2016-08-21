namespace ProcessingTools.Data.Common.Repositories.Contracts
{
    using System.Collections.Generic;

    public interface IIterableRepository<T> : IRepository<T>
    {
        IEnumerable<T> Entities { get; }
    }
}
