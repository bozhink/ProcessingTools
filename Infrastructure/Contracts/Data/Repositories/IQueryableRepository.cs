namespace ProcessingTools.Contracts.Data.Repositories
{
    using System.Linq;

    public interface IQueryableRepository<T> : IRepository<T>
    {
        IQueryable<T> Query { get; }
    }
}
