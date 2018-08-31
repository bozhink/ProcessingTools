// <copyright file="AbstractAddresssableDataService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Abstractions.Journals
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Code.Data.Expressions;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Contracts;
    using ProcessingTools.Data.Contracts.Journals;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Extensions.Linq.Expressions;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Models.Contracts.Services.Data.Journals;
    using ProcessingTools.Services.Models.Data.Journals;

    /// <summary>
    /// Abstract addresssable data service.
    /// </summary>
    /// <typeparam name="TServiceModel">Type of service model.</typeparam>
    /// <typeparam name="TDetailedServiceModel">Type of detailed service model.</typeparam>
    /// <typeparam name="TDataModel">Type of data model.</typeparam>
    /// <typeparam name="TRepository">Type of repository.</typeparam>
    public abstract class AbstractAddresssableDataService<TServiceModel, TDetailedServiceModel, TDataModel, TRepository>
        where TServiceModel : class, IServiceModel
        where TDetailedServiceModel : class, TServiceModel, IDetailedModel, ProcessingTools.Models.Contracts.Services.Data.Journals.IAddressable
        where TDataModel : class, IDataModel, ICreated, IModified, ProcessingTools.Models.Contracts.Journals.IAddressable
        where TRepository : class, ICrudRepository<TDataModel>, IAddressableRepository
    {
        private static readonly ConcurrentDictionary<string, Expression<Func<TDataModel, object>>> SortExpressions = new ConcurrentDictionary<string, Expression<Func<TDataModel, object>>>();

        private readonly TRepository repository;
        private readonly IApplicationContext applicationContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractAddresssableDataService{TServiceModel, TDetailedServiceModel, TDataModel, TRepository}"/> class.
        /// </summary>
        /// <param name="repository">The repository</param>
        /// <param name="applicationContext">The application context.</param>
        protected AbstractAddresssableDataService(TRepository repository, IApplicationContext applicationContext)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
        }

        /// <summary>
        /// Gets the mapping from the data model to the service model.
        /// </summary>
        protected abstract Func<TDataModel, TServiceModel> MapDataModelToServiceModel { get; }

        /// <summary>
        /// Gets the mapping from the data model to the detailed service model.
        /// </summary>
        protected abstract Func<TDataModel, TDetailedServiceModel> MapDataModelToDetailedServiceModel { get; }

        /// <summary>
        /// Gets the application context.
        /// </summary>
        protected IApplicationContext ApplicationContext => this.applicationContext;

        /// <summary>
        /// Gets the repository.
        /// </summary>
        protected TRepository Repository => this.repository;

        /// <summary>
        /// Adds new model.
        /// </summary>
        /// <param name="userId">User ID for validation.</param>
        /// <param name="model">Model to be added.</param>
        /// <returns>Task of result.</returns>
        public abstract Task<object> AddAsync(object userId, TServiceModel model);

        /// <summary>
        /// Updates existing model.
        /// </summary>
        /// <param name="userId">User ID for validation.</param>
        /// <param name="model">Model to be updated.</param>
        /// <returns>Task of result.</returns>
        public abstract Task<object> UpdateAsync(object userId, TServiceModel model);

        /// <summary>
        /// Deletes model by ID;
        /// </summary>
        /// <param name="userId">User ID for validation.</param>
        /// <param name="id">ID of the model to be deleted.</param>
        /// <returns>Task of result.</returns>
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

        /// <summary>
        /// Add address to model.
        /// </summary>
        /// <param name="userId">User ID for validation.</param>
        /// <param name="modelId">Model ID to be updated.</param>
        /// <param name="address"><see cref="IAddress"/> model to be attached.</param>
        /// <returns>Task of result.</returns>
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

            var now = this.applicationContext.DateTimeProvider.Invoke();
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

        /// <summary>
        /// Updates address of model.
        /// </summary>
        /// <param name="userId">User ID for validation.</param>
        /// <param name="modelId">Model ID to be updated.</param>
        /// <param name="address"><see cref="IAddress"/> model to be updated.</param>
        /// <returns>Task of result.</returns>
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

            var now = this.applicationContext.DateTimeProvider.Invoke();
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

        /// <summary>
        /// Removes address from model.
        /// </summary>
        /// <param name="userId">User ID for validation.</param>
        /// <param name="modelId">Model ID to be updated.</param>
        /// <param name="addressId">Address ID to be removed.</param>
        /// <returns>Task of result.</returns>
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

            var now = this.applicationContext.DateTimeProvider.Invoke();
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

        /// <summary>
        /// Gets model by ID.
        /// </summary>
        /// <param name="userId">User ID for validation.</param>
        /// <param name="id">ID of the model to be retrieved.</param>
        /// <returns>The model.</returns>
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

        /// <summary>
        /// Gets details of a specified model.
        /// </summary>
        /// <param name="userId">User ID for validation.</param>
        /// <param name="id">ID of the model to be retrieved.</param>
        /// <returns>Detailed model.</returns>
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

        /// <summary>
        /// Gets paged and sorted data.
        /// </summary>
        /// <param name="userId">User ID for validation.</param>
        /// <param name="skip">Number of items to be skipped.</param>
        /// <param name="take">Number of items to be taken.</param>
        /// <param name="sort">Sorting expression.</param>
        /// <param name="order"><see cref="SortOrder"/> for sorting.</param>
        /// <param name="filter">Filter expression.</param>
        /// <returns>Pages and sorted models.</returns>
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

            var data = query.Select(this.MapDataModelToServiceModel).ToList();

            return data;
        }

        /// <summary>
        /// Gets paged and sorted details.
        /// </summary>
        /// <param name="userId">User ID for validation.</param>
        /// <param name="skip">Number of items to be skipped.</param>
        /// <param name="take">Number of items to be taken.</param>
        /// <param name="sort">Sorting expression.</param>
        /// <param name="order"><see cref="SortOrder"/> for sorting.</param>
        /// <param name="filter">Filter expression.</param>
        /// <returns>Pages and sorted detailed models.</returns>
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

            var data = query.Select(this.MapDataModelToDetailedServiceModel).ToList();

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
