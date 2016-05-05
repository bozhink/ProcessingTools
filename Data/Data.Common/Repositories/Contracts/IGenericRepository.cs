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

        Task<object> Add(T entity);

        Task<object> Update(T entity);

        Task<object> Delete(T entity);

        Task<object> Delete(object id);

        Task<int> SaveChanges();
    }
}
