namespace ProcessingTools.Data.Common.Repositories.Contracts
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IIterableRepository<T> : IRepository<T>
    {
        IEnumerable<T> Entities { get; }
    }
}
