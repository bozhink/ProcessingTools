namespace ProcessingTools.Contracts.Data.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using ProcessingTools.Constants;
    using ProcessingTools.Enumerations;

    public interface ISearchableRepository<T> : IRepository<T>, IQueryableRepository<T>
    {
        Task<IQueryable<T>> Find(
            Expression<Func<T, bool>> filter);

        Task<IQueryable<T>> Find(
            Expression<Func<T, bool>> filter,
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
