namespace ProcessingTools.Journals.Services.Data.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Common.Expressions;
    using ProcessingTools.History.Services.Data.Contracts.Services;
    using ProcessingTools.Journals.Services.Data.Abstractions.Services;
    using ProcessingTools.Journals.Services.Data.Contracts.Models;
    using ProcessingTools.Journals.Services.Data.Contracts.Services;
    using ProcessingTools.Journals.Services.Data.Models.DataModels;
    using ProcessingTools.Journals.Services.Data.Models.ServiceModels;
    using TDataModel = ProcessingTools.Contracts.Data.Journals.Models.IPublisher;
    using TDetailedServiceModel = ProcessingTools.Journals.Services.Data.Contracts.Models.IPublisherDetails;
    using TRepository = ProcessingTools.Contracts.Data.Journals.Repositories.IPublishersRepository;
    using TServiceModel = ProcessingTools.Journals.Services.Data.Contracts.Models.IPublisher;

    public class PublishersDataService : AbstractAddresssableDataService<TServiceModel, TDetailedServiceModel, TDataModel, TRepository>, IPublishersDataService
    {
        private readonly IHistoryDataService historyService;

        public PublishersDataService(TRepository repository, IDateTimeProvider datetimeProvider, IHistoryDataService historyService)
            : base(repository, datetimeProvider)
        {
            this.historyService = historyService ?? throw new ArgumentNullException(nameof(historyService));
        }

        public bool SaveToHistory { get; set; } = true;

        protected override Func<TDataModel, TDetailedServiceModel> MapDataModelToDetailedServiceModel => dataModel => new PublisherDetailsServiceModel
        {
            Id = dataModel.Id,
            AbbreviatedName = dataModel.AbbreviatedName,
            Name = dataModel.Name,
            CreatedByUser = dataModel.CreatedByUser,
            DateCreated = dataModel.DateCreated,
            DateModified = dataModel.DateModified,
            ModifiedByUser = dataModel.ModifiedByUser,
            Addresses = dataModel.Addresses
                .Select(a => new AddressServiceModel
                {
                    AddressString = a.AddressString,
                    CityId = a.CityId,
                    CountryId = a.CountryId,
                    Id = a.Id
                })
                .ToList<IAddress>()
        };

        protected override Func<TDataModel, TServiceModel> MapDataModelToServiceModel => dataModel => new PublisherServiceModel
        {
            Id = dataModel.Id,
            AbbreviatedName = dataModel.AbbreviatedName,
            Name = dataModel.Name
        };

        public override async Task<object> Add(object userId, TServiceModel model)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var now = this.DatetimeProvider.Now;
            var user = userId.ToString();
            var dataModel = new PublisherDataModel
            {
                AbbreviatedName = model.AbbreviatedName,
                Name = model.Name,
                CreatedByUser = user,
                ModifiedByUser = user,
                DateCreated = now,
                DateModified = now
            };

            await this.Repository.AddAsync(dataModel).ConfigureAwait(false);
            await this.Repository.SaveChangesAsync().ConfigureAwait(false);

            if (this.SaveToHistory)
            {
                var entity = await this.Repository.GetByIdAsync(dataModel.Id).ConfigureAwait(false);
                await this.historyService.AddItemToHistory(userId, entity.Id, entity).ConfigureAwait(false);
            }

            return dataModel.Id;
        }

        public override async Task<object> Update(object userId, TServiceModel model)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var now = this.DatetimeProvider.Now;
            var user = userId.ToString();

            await this.Repository.UpdateAsync(
                model.Id,
                ExpressionBuilder<TDataModel>
                    .UpdateExpression
                    .Set(p => p.Name, model.Name)
                    .Set(p => p.AbbreviatedName, model.AbbreviatedName)
                    .Set(p => p.ModifiedByUser, user)
                    .Set(p => p.DateModified, now))
                .ConfigureAwait(false);

            await this.Repository.SaveChangesAsync().ConfigureAwait(false);

            if (this.SaveToHistory)
            {
                var entity = await this.Repository.GetByIdAsync(model.Id).ConfigureAwait(false);
                await this.historyService.AddItemToHistory(userId, entity.Id, entity).ConfigureAwait(false);
            }

            return model.Id;
        }

        public override async Task<object> AddAddress(object userId, object modelId, IAddress address)
        {
            var result = await base.AddAddress(userId, modelId, address).ConfigureAwait(false);

            if (this.SaveToHistory)
            {
                var entity = await this.Repository.GetByIdAsync(modelId).ConfigureAwait(false);
                await this.historyService.AddItemToHistory(userId, entity.Id, entity).ConfigureAwait(false);
            }

            return result;
        }

        public override async Task<object> UpdateAddress(object userId, object modelId, IAddress address)
        {
            var result = await base.UpdateAddress(userId, modelId, address).ConfigureAwait(false);

            if (this.SaveToHistory)
            {
                var entity = await this.Repository.GetByIdAsync(modelId).ConfigureAwait(false);
                await this.historyService.AddItemToHistory(userId, entity.Id, entity).ConfigureAwait(false);
            }

            return result;
        }

        public override async Task<object> RemoveAddress(object userId, object modelId, object addressId)
        {
            var result = await base.RemoveAddress(userId, modelId, addressId).ConfigureAwait(false);

            if (this.SaveToHistory)
            {
                var entity = await this.Repository.GetByIdAsync(modelId).ConfigureAwait(false);
                await this.historyService.AddItemToHistory(userId, entity.Id, entity).ConfigureAwait(false);
            }

            return result;
        }
    }
}
