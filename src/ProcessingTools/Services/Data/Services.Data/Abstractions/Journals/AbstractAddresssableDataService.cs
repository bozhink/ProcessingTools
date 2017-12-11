namespace ProcessingTools.Journals.Services.Data.Abstractions.Services
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Extensions.Linq;
    using ProcessingTools.Common.Extensions.Linq.Expressions;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Contracts.Data.Repositories.Journals;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Services.Data.Journals;
    using ProcessingTools.Data.Common.Expressions;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Exceptions;
    using ProcessingTools.Services.Models.Data.Journals;

    public abstract class AbstractAddresssableDataService<TServiceModel, TDetailedServiceModel, TDataModel, TRepository>
        where TServiceModel : class, IServiceModel
        where TDetailedServiceModel : class, TServiceModel, IDetailedModel, ProcessingTools.Contracts.Models.Services.Data.Journals.IAddressable
        where TDataModel : class, IDataModel, ICreated, IModified, ProcessingTools.Contracts.Models.Journals.IAddressable
        where TRepository : class, ICrudRepository<TDataModel>, IAddressableRepository
    {
        private static readonly ConcurrentDictionary<string, Expression<Func<TDataModel, object>>> SortExpressions = new ConcurrentDictionary<string, Expression<Func<TDataModel, object>>>();

        private readonly TRepository repository;
        private readonly IDateTimeProvider datetimeProvider;

        protected AbstractAddresssableDataService(TRepository repository, IDateTimeProvider datetimeProvider)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.datetimeProvider = datetimeProvider ?? throw new ArgumentNullException(nameof(datetimeProvider));
        }

        protected abstract Func<TDataModel, TServiceModel> MapDataModelToServiceModel { get; }

        protected abstract Func<TDataModel, TDetailedServiceModel> MapDataModelToDetailedServiceModel { get; }

        protected IDateTimeProvider DatetimeProvider => this.datetimeProvider;

        protected TRepository Repository => this.repository;

        public abstract Task<object> AddAsync(object userId, TServiceModel model);

        public abstract Task<object> UpdateAsync(object userId, TServiceModel model);

        public virtual async Task<object> DeleteAsync(object userId, object id)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var result = await this.repository.DeleteAsync(id).ConfigureAwait(false);

            await this.repository.SaveChangesAsync().ConfigureAwait(false);

            return result;
        }

        public virtual async Task<object> AddAddressAsync(object userId, object modelId, IAddress address)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (modelId == null)
            {
                throw new ArgumentNullException(nameof(modelId));
            }

            if (address == null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            var dataModel = new AddressDataModel
            {
                AddressString = address.AddressString,
                CityId = address.CityId,
                CountryId = address.CountryId
            };

            await this.repository.AddAddress(modelId, dataModel).ConfigureAwait(false);

            var now = this.datetimeProvider.Now;
            var user = userId.ToString();

            await this.repository.UpdateAsync(
                modelId,
                ExpressionBuilder<TDataModel>
                    .UpdateExpression
                    .Set(p => p.ModifiedBy, user)
                    .Set(p => p.ModifiedOn, now))
                .ConfigureAwait(false);

            await this.repository.SaveChangesAsync().ConfigureAwait(false);

            return dataModel.Id;
        }

        public virtual async Task<object> UpdateAddressAsync(object userId, object modelId, IAddress address)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (modelId == null)
            {
                throw new ArgumentNullException(nameof(modelId));
            }

            if (address == null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            var dataModel = new AddressDataModel
            {
                Id = address.Id,
                AddressString = address.AddressString,
                CityId = address.CityId,
                CountryId = address.CountryId
            };

            await this.repository.UpdateAddress(modelId, dataModel).ConfigureAwait(false);

            var now = this.datetimeProvider.Now;
            var user = userId.ToString();

            await this.repository.UpdateAsync(
                modelId,
                ExpressionBuilder<TDataModel>
                    .UpdateExpression
                    .Set(p => p.ModifiedBy, user)
                    .Set(p => p.ModifiedOn, now))
                .ConfigureAwait(false);

            await this.repository.SaveChangesAsync().ConfigureAwait(false);

            return dataModel.Id;
        }

        public virtual async Task<object> RemoveAddressAsync(object userId, object modelId, object addressId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (modelId == null)
            {
                throw new ArgumentNullException(nameof(modelId));
            }

            if (addressId == null)
            {
                throw new ArgumentNullException(nameof(addressId));
            }

            await this.repository.RemoveAddress(modelId, addressId).ConfigureAwait(false);

            var now = this.datetimeProvider.Now;
            var user = userId.ToString();

            await this.repository.UpdateAsync(
                modelId,
                ExpressionBuilder<TDataModel>
                    .UpdateExpression
                    .Set(p => p.ModifiedBy, user)
                    .Set(p => p.ModifiedOn, now))
                .ConfigureAwait(false);

            await this.repository.SaveChangesAsync().ConfigureAwait(false);

            return addressId;
        }

        public virtual async Task<TServiceModel> GetAsync(object userId, object id)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var dataModel = await this.repository.GetByIdAsync(id).ConfigureAwait(false);
            if (dataModel == null)
            {
                return null;
            }

            var model = this.MapDataModelToServiceModel(dataModel);

            return model;
        }

        public async Task<TDetailedServiceModel> GetDetailsAsync(object userId, object id)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var dataModel = await this.repository.GetByIdAsync(id).ConfigureAwait(false);
            if (dataModel == null)
            {
                return null;
            }

            var model = this.MapDataModelToDetailedServiceModel(dataModel);

            return model;
        }

        public async Task<IEnumerable<TServiceModel>> SelectAsync(object userId, int skip, int take, Expression<Func<TServiceModel, object>> sort, SortOrder order = SortOrder.Ascending, Expression<Func<TServiceModel, bool>> filter = null)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (skip < 0)
            {
                throw new InvalidSkipValuePagingException();
            }

            if (take > PaginationConstants.MaximalItemsPerPageAllowed || take < 1)
            {
                throw new InvalidTakeValuePagingException();
            }

            if (sort == null)
            {
                throw new ArgumentNullException(nameof(sort));
            }

            var query = await this.BuildQueryAsync(skip, take, sort, order, filter).ConfigureAwait(false);

            var data = await query.Select(this.MapDataModelToServiceModel).ToListAsync().ConfigureAwait(false);

            return data;
        }

        public async Task<IEnumerable<TDetailedServiceModel>> SelectDetailsAsync(object userId, int skip, int take, Expression<Func<TDetailedServiceModel, object>> sort, SortOrder order = SortOrder.Ascending, Expression<Func<TDetailedServiceModel, bool>> filter = null)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (skip < 0)
            {
                throw new InvalidSkipValuePagingException();
            }

            if (take > PaginationConstants.MaximalItemsPerPageAllowed || take < 1)
            {
                throw new InvalidTakeValuePagingException();
            }

            if (sort == null)
            {
                throw new ArgumentNullException(nameof(sort));
            }

            var query = await this.BuildQueryAsync(skip, take, sort, order, filter).ConfigureAwait(false);

            var data = await query.Select(this.MapDataModelToDetailedServiceModel).ToListAsync().ConfigureAwait(false);

            return data;
        }

        private async Task<IEnumerable<TDataModel>> BuildQueryAsync(int skip, int take, LambdaExpression sort, SortOrder order, LambdaExpression filter)
        {
            Expression<Func<TDataModel, bool>> dataFilter = x => true;
            try
            {
                if (filter != null)
                {
                    dataFilter = filter.ToExpression<TDataModel, bool>();
                }
            }
            catch
            {
                // Skip
            }

            IEnumerable<TDataModel> query = await this.repository.FindAsync(dataFilter).ConfigureAwait(false);

            try
            {
                var lambda = SortExpressions.GetOrAdd(
                    sort.ToString(),
                    key => sort.ToExpression<TDataModel, object>());

                var func = lambda.Compile();

                switch (order)
                {
                    case SortOrder.Ascending:
                        query = query.OrderBy(func);
                        break;

                    case SortOrder.Descending:
                        query = query.OrderByDescending(func);
                        break;

                    default:
                        break;
                }
            }
            catch
            {
                // Skip
            }

            query = query.Skip(skip).Take(take);
            return query;
        }
    }
}
