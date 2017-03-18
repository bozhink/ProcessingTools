namespace ProcessingTools.Journals.Services.Data.Abstractions.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Constants;
    using Contracts.Models;
    using Models.DataModels;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Data.Common.Expressions;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Exceptions;
    using ProcessingTools.Extensions.Linq;
    using ProcessingTools.Journals.Data.Common.Contracts.Repositories;

    public abstract class AbstractAddresssableDataService<TServiceModel, TDetailedServiceModel, TDataModel, TRepository>
        where TServiceModel : class, IServiceModel
        where TDetailedServiceModel : class, TServiceModel, IDetailedModel, Contracts.Models.IAddressable
        where TDataModel : class, IDataModel, IModelWithUserInformation, ProcessingTools.Journals.Data.Common.Contracts.Models.IAddressable
        where TRepository : ISearchableCountableCrudRepository<TDataModel>, IAddressableRepository
    {
        private readonly TRepository repository;
        private readonly IDateTimeProvider datetimeProvider;

        public AbstractAddresssableDataService(TRepository repository, IDateTimeProvider datetimeProvider)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            if (datetimeProvider == null)
            {
                throw new ArgumentNullException(nameof(datetimeProvider));
            }

            this.repository = repository;
            this.datetimeProvider = datetimeProvider;
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

            await this.repository.SaveChanges();

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

            await this.repository.SaveChanges();

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

            await this.repository.SaveChanges();

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

            if (PagingConstants.MaximalItemsPerPageAllowed < take || take < 1)
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

            if (PagingConstants.MaximalItemsPerPageAllowed < take || take < 1)
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
            if (filter != null)
            {
                ParameterExpression filterParameter = filter.Parameters.Single();
                ParameterExpression parameter = Expression.Parameter(typeof(TDataModel), filterParameter.Name);

                dataFilter = Expression.Lambda<Func<TDataModel, bool>>(filter.Body, parameter);
            }

            var query = await this.repository.Find(dataFilter);

            if (sort != null)
            {
                ParameterExpression sortParameter = sort.Parameters.Single();
                ParameterExpression parameter = Expression.Parameter(typeof(TDataModel), sortParameter.Name);
                var lambda = Expression.Lambda<Func<TDataModel, object>>(sort.Body, parameter).Compile();

                switch (order)
                {
                    case SortOrder.Ascending:
                        query = query.OrderBy(lambda);
                        break;

                    case SortOrder.Descending:
                        query = query.OrderByDescending(lambda);
                        break;

                    default:
                        break;
                }
            }

            query = query.Skip(skip).Take(take);
            return query;
        }
    }
}
