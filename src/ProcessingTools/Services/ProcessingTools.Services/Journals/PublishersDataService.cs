// <copyright file="PublishersDataService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Journals
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Data.Expressions;
    using ProcessingTools.Contracts;
    using ProcessingTools.Models.Contracts.Services.Data.Journals;
    using ProcessingTools.Services.Abstractions.Journals;
    using ProcessingTools.Services.Contracts.History;
    using ProcessingTools.Services.Contracts.Journals;
    using ProcessingTools.Services.Models.Data.Journals;
    using TDataModel = ProcessingTools.Models.Contracts.Journals.IPublisher;
    using TDetailedServiceModel = ProcessingTools.Models.Contracts.Services.Data.Journals.IPublisherDetails;
    using TRepository = ProcessingTools.Data.Contracts.Journals.IPublishersRepository;
    using TServiceModel = ProcessingTools.Models.Contracts.Services.Data.Journals.IPublisher;

    /// <summary>
    /// Publishers data service.
    /// </summary>
    public class PublishersDataService : AbstractAddresssableDataService<TServiceModel, TDetailedServiceModel, TDataModel, TRepository>, IPublishersDataService
    {
        private readonly IObjectHistoryDataService objectHistoriesService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishersDataService"/> class.
        /// </summary>
        /// <param name="repository">Instance of <see cref="ProcessingTools.Data.Contracts.Journals.IPublishersRepository"/>.</param>
        /// <param name="applicationContext">Application context.</param>
        /// <param name="objectHistoriesService">Instance of <see cref="IObjectHistoryDataService"/>.</param>
        public PublishersDataService(TRepository repository, IApplicationContext applicationContext, IObjectHistoryDataService objectHistoriesService)
            : base(repository, applicationContext)
        {
            this.objectHistoriesService = objectHistoriesService ?? throw new ArgumentNullException(nameof(objectHistoriesService));
        }

        /// <inheritdoc/>
        public bool SaveToHistory { get; set; } = true;

        /// <inheritdoc/>
        protected override Func<TDataModel, TDetailedServiceModel> MapDataModelToDetailedServiceModel => dataModel => new PublisherDetails
        {
            Id = dataModel.Id,
            AbbreviatedName = dataModel.AbbreviatedName,
            Name = dataModel.Name,
            CreatedBy = dataModel.CreatedBy,
            CreatedOn = dataModel.CreatedOn,
            ModifiedOn = dataModel.ModifiedOn,
            ModifiedBy = dataModel.ModifiedBy,
            Addresses = dataModel.Addresses
                .Select(a => new Address
                {
                    AddressString = a.AddressString,
                    CityId = a.CityId,
                    CountryId = a.CountryId,
                    Id = a.Id
                })
                .ToList<IAddress>()
        };

        /// <inheritdoc/>
        protected override Func<TDataModel, TServiceModel> MapDataModelToServiceModel => dataModel => new Publisher
        {
            Id = dataModel.Id,
            AbbreviatedName = dataModel.AbbreviatedName,
            Name = dataModel.Name
        };

        /// <inheritdoc/>
        public override async Task<object> AddAsync(object userId, TServiceModel model)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var now = this.ApplicationContext.DateTimeProvider.Invoke();
            var user = userId.ToString();
            var dataModel = new PublisherDataModel
            {
                AbbreviatedName = model.AbbreviatedName,
                Name = model.Name,
                CreatedBy = user,
                ModifiedBy = user,
                CreatedOn = now,
                ModifiedOn = now
            };

            await this.Repository.AddAsync(dataModel).ConfigureAwait(false);
            await this.Repository.SaveChangesAsync().ConfigureAwait(false);

            if (this.SaveToHistory)
            {
                var entity = await this.Repository.GetByIdAsync(dataModel.Id).ConfigureAwait(false);
                await this.objectHistoriesService.AddAsync(entity.Id, entity).ConfigureAwait(false);
            }

            return dataModel.Id;
        }

        /// <inheritdoc/>
        public override async Task<object> UpdateAsync(object userId, TServiceModel model)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var now = this.ApplicationContext.DateTimeProvider.Invoke();
            var user = userId.ToString();

            await this.Repository.UpdateAsync(
                model.Id,
                ExpressionBuilder<TDataModel>
                    .UpdateExpression
                    .Set(p => p.Name, model.Name)
                    .Set(p => p.AbbreviatedName, model.AbbreviatedName)
                    .Set(p => p.ModifiedBy, user)
                    .Set(p => p.ModifiedOn, now))
                .ConfigureAwait(false);

            await this.Repository.SaveChangesAsync().ConfigureAwait(false);

            if (this.SaveToHistory)
            {
                var entity = await this.Repository.GetByIdAsync(model.Id).ConfigureAwait(false);
                await this.objectHistoriesService.AddAsync(entity.Id, entity).ConfigureAwait(false);
            }

            return model.Id;
        }

        /// <inheritdoc/>
        public override async Task<object> AddAddressAsync(object userId, object modelId, IAddress address)
        {
            var result = await base.AddAddressAsync(userId, modelId, address).ConfigureAwait(false);

            if (this.SaveToHistory)
            {
                var entity = await this.Repository.GetByIdAsync(modelId).ConfigureAwait(false);
                await this.objectHistoriesService.AddAsync(entity.Id, entity).ConfigureAwait(false);
            }

            return result;
        }

        /// <inheritdoc/>
        public override async Task<object> UpdateAddressAsync(object userId, object modelId, IAddress address)
        {
            var result = await base.UpdateAddressAsync(userId, modelId, address).ConfigureAwait(false);

            if (this.SaveToHistory)
            {
                var entity = await this.Repository.GetByIdAsync(modelId).ConfigureAwait(false);
                await this.objectHistoriesService.AddAsync(entity.Id, entity).ConfigureAwait(false);
            }

            return result;
        }

        /// <inheritdoc/>
        public override async Task<object> RemoveAddressAsync(object userId, object modelId, object addressId)
        {
            var result = await base.RemoveAddressAsync(userId, modelId, addressId).ConfigureAwait(false);

            if (this.SaveToHistory)
            {
                var entity = await this.Repository.GetByIdAsync(modelId).ConfigureAwait(false);
                await this.objectHistoriesService.AddAsync(entity.Id, entity).ConfigureAwait(false);
            }

            return result;
        }
    }
}
