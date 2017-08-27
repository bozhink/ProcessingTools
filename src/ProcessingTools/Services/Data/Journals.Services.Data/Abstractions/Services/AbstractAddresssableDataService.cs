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
    using ProcessingTools.Contracts.Data.Journals.Repositories;
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Data.Common.Expressions;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Exceptions;
    using ProcessingTools.Journals.Services.Data.Contracts.Models;
    using ProcessingTools.Journals.Services.Data.Models.DataModels;

    public abstract class AbstractAddresssableDataService<TServiceModel, TDetailedServiceModel, TDataModel, TRepository>
        where TServiceModel : class, IServiceModel
        where TDetailedServiceModel : class, TServiceModel, IDetailedModel, Contracts.Models.IAddressable
        where TDataModel : class, IDataModel, IModelWithUserInformation, ProcessingTools.Contracts.Data.Journals.Models.IAddressable
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

        public abstract Task<object> Add(object userId, TServiceModel model);

        public abstract Task<object> Update(object userId, TServiceModel model);

        public virtual async Task<object> Delete(object userId, object id)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var result = await this.repository.Delete(id);

            await this.repository.SaveChangesAsync();

            return result;
        }

        public virtual async Task<object> AddAddress(object userId, object modelId, IAddress address)
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

            await this.repository.AddAddress(modelId, dataModel);

            var now = this.datetimeProvider.Now;
            var user = userId.ToString();

            await this.repository.Update(
                modelId,
                ExpressionBuilder<TDataModel>
                    .UpdateExpression
                    .Set(p => p.ModifiedByUser, user)
                    .Set(p => p.DateModified, now));

            await this.repository.SaveChangesAsync();

            return dataModel.Id;
        }

        public virtual async Task<object> UpdateAddress(object userId, object modelId, IAddress address)
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

            await this.repository.UpdateAddress(modelId, dataModel);

            var now = this.datetimeProvider.Now;
            var user = userId.ToString();

            await this.repository.Update(
                modelId,
                ExpressionBuilder<TDataModel>
                    .UpdateExpression
                    .Set(p => p.ModifiedByUser, user)
                    .Set(p => p.DateModified, now));

            await this.repository.SaveChangesAsync();

            return dataModel.Id;
        }

        public virtual async Task<object> RemoveAddress(object userId, object modelId, object addressId)
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

            await this.repository.RemoveAddress(modelId, addressId);

            var now = this.datetimeProvider.Now;
            var user = userId.ToString();

            await this.repository.Update(
                modelId,
                ExpressionBuilder<TDataModel>
                    .UpdateExpression
                    .Set(p => p.ModifiedByUser, user)
                    .Set(p => p.DateModified, now));

            await this.repository.SaveChangesAsync();

            return addressId;
        }

        public virtual async Task<TServiceModel> Get(object userId, object id)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var dataModel = await this.repository.GetById(id);
            if (dataModel == null)
            {
                return null;
            }

            var model = this.MapDataModelToServiceModel(dataModel);

            return model;
        }

        public async Task<TDetailedServiceModel> GetDetails(object userId, object id)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var dataModel = await this.repository.GetById(id);
            if (dataModel == null)
            {
                return null;
            }

            var model = this.MapDataModelToDetailedServiceModel(dataModel);

            return model;
        }

        public async Task<IEnumerable<TServiceModel>> Select(object userId, int skip, int take, Expression<Func<TServiceModel, object>> sort, SortOrder order = SortOrder.Ascending, Expression<Func<TServiceModel, bool>> filter = null)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (skip < 0)
            {
                throw new InvalidSkipValuePagingException();
            }

            if (take > PagingConstants.MaximalItemsPerPageAllowed || take < 1)
            {
                throw new InvalidTakeValuePagingException();
            }

            if (sort == null)
            {
                throw new ArgumentNullException(nameof(sort));
            }

            var query = await this.BuildQuery(skip, take, sort, order, filter);

            var data = await query.Select(this.MapDataModelToServiceModel).ToListAsync();

            return data;
        }

        public async Task<IEnumerable<TDetailedServiceModel>> SelectDetails(object userId, int skip, int take, Expression<Func<TDetailedServiceModel, object>> sort, SortOrder order = SortOrder.Ascending, Expression<Func<TDetailedServiceModel, bool>> filter = null)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (skip < 0)
            {
                throw new InvalidSkipValuePagingException();
            }

            if (take > PagingConstants.MaximalItemsPerPageAllowed || take < 1)
            {
                throw new InvalidTakeValuePagingException();
            }

            if (sort == null)
            {
                throw new ArgumentNullException(nameof(sort));
            }

            var query = await this.BuildQuery(skip, take, sort, order, filter);

            var data = await query.Select(this.MapDataModelToDetailedServiceModel).ToListAsync();

            return data;
        }

        private async Task<IEnumerable<TDataModel>> BuildQuery(int skip, int take, LambdaExpression sort, SortOrder order, LambdaExpression filter)
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

            var query = await this.repository.Find(dataFilter);

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
