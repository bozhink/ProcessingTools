namespace ProcessingTools.Data.Common.Repositories.Contracts
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Types;

    public interface ISearchableRepository<TEntity>
    {
        Task<IQueryable<TEntity>> Find(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, object>> sort,
            SortOrder sortOrder = SortOrder.Ascending,
            int skip = 0,
            int take = PagingConstants.DefaultNumberOfTopItemsToSelect);

        Task<IQueryable<T>> Find<T>(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, T>> projection,
            Expression<Func<TEntity, object>> sort,
            SortOrder sortOrder = SortOrder.Ascending,
            int skip = 0,
            int take = PagingConstants.DefaultNumberOfTopItemsToSelect);

        Task<TEntity> FindFirst(
            Expression<Func<TEntity, bool>> filter);

        Task<T> FindFirst<T>(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, T>> projection);
    }
}
