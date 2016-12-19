namespace ProcessingTools.Contracts.Data.Repositories
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface ISearchableRepository<T> : IRepository<T>, IQueryableRepository<T>, IFiltrableRepository<T>
    {
        Task<Tout> FindFirst<Tout>(
            Expression<Func<T, bool>> filter,
            Expression<Func<T, Tout>> projection);

        Task<T> Get(object id);
    }
}
