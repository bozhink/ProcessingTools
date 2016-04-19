namespace ProcessingTools.Data.Common.Repositories.Contracts
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IGenericRepository<T>
    {
        Task<IQueryable<T>> All();

        Task<IQueryable<T>> All(Expression<Func<T, bool>> filter);

        Task<IQueryable<T>> All(Expression<Func<T, object>> sort, int skip, int take);

        Task<IQueryable<T>> All(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, int skip, int take);

        Task<T> Get(object id);

        Task<T> Add(T entity);

        Task<T> Update(T entity);

        Task<T> Delete(T entity);

        Task<T> Delete(object id);

        Task<int> SaveChanges();
    }
}
