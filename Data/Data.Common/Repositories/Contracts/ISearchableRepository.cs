namespace ProcessingTools.Data.Common.Repositories.Contracts
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using ProcessingTools.Common.Types;
    using ProcessingTools.Constants;

    public interface ISearchableRepository<T> : IRepository<T>
    {
        Task<IQueryable<T>> All();

        Task<IQueryable<T>> Find(
            Expression<Func<T, bool>> filter);

        Task<IQueryable<Tout>> Find<Tout>(
            Expression<Func<T, bool>> filter,
            Expression<Func<T, Tout>> projection);

        Task<IQueryable<T>> Find(
            Expression<Func<T, bool>> filter,
            Expression<Func<T, object>> sort,
            SortOrder sortOrder = SortOrder.Ascending,
            int skip = 0,
            int take = PagingConstants.DefaultNumberOfTopItemsToSelect);

        Task<IQueryable<Tout>> Find<Tout>(
            Expression<Func<T, bool>> filter,
            Expression<Func<T, Tout>> projection,
            Expression<Func<T, object>> sort,
            SortOrder sortOrder = SortOrder.Ascending,
            int skip = 0,
            int take = PagingConstants.DefaultNumberOfTopItemsToSelect);

        Task<T> FindFirst(
            Expression<Func<T, bool>> filter);

        Task<Tout> FindFirst<Tout>(
            Expression<Func<T, bool>> filter,
            Expression<Func<T, Tout>> projection);

        Task<T> Get(object id);
    }
}
