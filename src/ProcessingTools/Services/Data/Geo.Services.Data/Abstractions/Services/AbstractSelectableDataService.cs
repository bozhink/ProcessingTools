namespace ProcessingTools.Geo.Services.Data.Abstractions.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services.Data;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Extensions.Linq;
    using ProcessingTools.Extensions.Linq.Expressions;
    using ProcessingTools.Geo.Data.Entity.Contracts.Repositories;

    public abstract class AbstractSelectableDataService<TServiceModel, TDataModel> : ISelectableDataService<TServiceModel>
        where TServiceModel : class, IServiceModel
        where TDataModel : class, IDataModel
    {
        private readonly IGeoDataRepository<TDataModel> repository;

        public AbstractSelectableDataService(IGeoDataRepository<TDataModel> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            this.repository = repository;
        }

        protected abstract Expression<Func<TDataModel, TServiceModel>> MapDataModelToServiceModel { get; }

        public virtual async Task<long> Count(Expression<Func<TServiceModel, bool>> filter = null)
        {
            var query = this.BaseQuery(filter);

            return await query.LongCountAsync();
        }

        public virtual async Task<IEnumerable<TServiceModel>> Select(Expression<Func<TServiceModel, bool>> filter = null)
        {
            var query = this.BaseQuery(filter).Select(this.MapDataModelToServiceModel);

            return await query.ToArrayAsync();
        }

        public virtual async Task<IEnumerable<TServiceModel>> Select(int skip, int take, Expression<Func<TServiceModel, object>> sort, SortOrder order = SortOrder.Ascending, Expression<Func<TServiceModel, bool>> filter = null)
        {
            if (skip < 0)
            {
                throw new InvalidSkipValuePagingException();
            }

            if (PagingConstants.MaximalItemsPerPageAllowed < take || take < 1)
            {
                throw new InvalidTakeValuePagingException();
            }

            if (sort == null)
            {
                throw new ArgumentNullException(nameof(sort));
            }

            var baseQuery = this.BaseQuery(filter);

            switch (order)
            {
                case SortOrder.Ascending:
                    baseQuery = baseQuery.OrderBy(sort.ToExpression<TServiceModel, TDataModel, object>());
                    break;

                case SortOrder.Descending:
                    baseQuery = baseQuery.OrderByDescending(sort.ToExpression<TServiceModel, TDataModel, object>());
                    break;

                default:
                    break;
            }

            var query = baseQuery.Skip(skip).Take(take).Select(this.MapDataModelToServiceModel);

            return await query.ToArrayAsync();
        }

        private IQueryable<TDataModel> BaseQuery(Expression<Func<TServiceModel, bool>> filter)
        {
            var query = this.repository.Query;

            if (filter != null)
            {
                query = query.Where(filter.ToExpression<TServiceModel, TDataModel, bool>());
            }

            return query;
        }
    }
}
