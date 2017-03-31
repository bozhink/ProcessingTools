namespace ProcessingTools.Geo.Services.Data.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using ProcessingTools.Constants;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Exceptions;
    using ProcessingTools.Extensions.Linq;
    using ProcessingTools.Extensions.Linq.Expressions;
    using ProcessingTools.Geo.Data.Entity.Contracts.Repositories;
    using ProcessingTools.Geo.Data.Entity.Models;
    using ProcessingTools.Geo.Services.Data.Contracts.Models;
    using ProcessingTools.Geo.Services.Data.Contracts.Services;
    using ProcessingTools.Geo.Services.Data.Models;

    public class CountriesSelectableDataService : ICountriesSelectableDataService
    {
        private readonly IGeoDataRepository<Country> repository;

        public CountriesSelectableDataService(IGeoDataRepository<Country> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            this.repository = repository;
        }

        public async Task<long> Count(Expression<Func<ICountryListableModel, bool>> filter = null)
        {
            var query = this.BaseQuery(filter);

            return await query.LongCountAsync();
        }

        public async Task<IEnumerable<ICountryListableModel>> Select(Expression<Func<ICountryListableModel, bool>> filter = null)
        {
            var query = this.BaseQuery(filter)
                .Select(c => new CountryListableModel
                {
                    Id = c.Id,
                    Name = c.Name
                });

            return await query.ToArrayAsync<ICountryListableModel>();
        }

        public async Task<IEnumerable<ICountryListableModel>> Select(int skip, int take, Expression<Func<ICountryListableModel, object>> sort, SortOrder order = SortOrder.Ascending, Expression<Func<ICountryListableModel, bool>> filter = null)
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
                    baseQuery = baseQuery.OrderBy(sort.ToExpression<Country, object>());
                    break;

                case SortOrder.Descending:
                    baseQuery = baseQuery.OrderByDescending(sort.ToExpression<Country, object>());
                    break;

                default:
                    break;
            }

            var query = baseQuery.Skip(skip).Take(take)
                .Select(c => new CountryListableModel
                {
                    Id = c.Id,
                    Name = c.Name
                });

            return await query.ToArrayAsync<ICountryListableModel>();
        }

        private IQueryable<Country> BaseQuery(Expression<Func<ICountryListableModel, bool>> filter)
        {
            var query = this.repository.Query;

            if (filter != null)
            {
                query = query.Where(filter.ToExpression<Country, bool>());
            }

            return query;
        }
    }
}
