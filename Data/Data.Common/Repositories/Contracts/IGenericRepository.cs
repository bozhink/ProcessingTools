namespace ProcessingTools.Data.Common.Repositories.Contracts
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Types;

    public interface IGenericRepository<TEntity>
    {
        Task<object> Add(TEntity entity);

        Task<IQueryable<TEntity>> All();

        Task<object> Delete(TEntity entity);

        Task<object> Delete(object id);

        Task<TEntity> Get(Expression<Func<TEntity, bool>> selector);

        Task<TEntity> Get(object id);

        Task<IQueryable<TEntity>> Query(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, object>> sort,
            int skip = 0,
            int take = PagingConstants.DefaultNumberOfTopItemsToSelect,
            SortOrder sortOrder = SortOrder.Ascending);

        Task<IQueryable<T>> Query<T>(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, T>> projection,
            Expression<Func<TEntity, object>> sort,
            int skip = 0,
            int take = PagingConstants.DefaultNumberOfTopItemsToSelect,
            SortOrder sortOrder = SortOrder.Ascending);

        Task<int> SaveChanges();

        Task<object> Update(TEntity entity);
    }
}
