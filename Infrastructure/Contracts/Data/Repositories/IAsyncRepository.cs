namespace ProcessingTools.Contracts.Data.Repositories
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IAsyncRepository<T> : ISearchableRepository<T>
    {
        Task<long> Count();

        Task<long> Count(Expression<Func<T, bool>> filter);

        Task<object> Add(T entity);

        Task<object> Delete(object id);

        Task<object> Update(T entity);
    }
}
