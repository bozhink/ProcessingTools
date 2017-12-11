namespace ProcessingTools.Journals.Services.Data.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Models.Services.Data.Journals;
    using ProcessingTools.Contracts.Services.Data.History;
    using ProcessingTools.Contracts.Services.Data.Journals;
    using ProcessingTools.Data.Common.Expressions;
    using ProcessingTools.Journals.Services.Data.Abstractions.Services;
    using ProcessingTools.Services.Models.Data.Journals;
    using TDataModel = ProcessingTools.Contracts.Models.Journals.IPublisher;
    using TDetailedServiceModel = ProcessingTools.Contracts.Models.Services.Data.Journals.IPublisherDetails;
    using TRepository = ProcessingTools.Contracts.Data.Repositories.Journals.IPublishersRepository;
    using TServiceModel = ProcessingTools.Contracts.Models.Services.Data.Journals.IPublisher;

    public class PublishersDataService : AbstractAddresssableDataService<TServiceModel, TDetailedServiceModel, TDataModel, TRepository>, IPublishersDataService
    {
        private readonly IObjectHistoriesDataService objectHistoriesService;

        public PublishersDataService(TRepository repository, IDateTimeProvider datetimeProvider, IObjectHistoriesDataService objectHistoriesService)
            : base(repository, datetimeProvider)
        {
            this.objectHistoriesService = objectHistoriesService ?? throw new ArgumentNullException(nameof(objectHistoriesService));
        }

        public bool SaveToHistory { get; set; } = true;

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

        protected override Func<TDataModel, TServiceModel> MapDataModelToServiceModel => dataModel => new Publisher
        {
            Id = dataModel.Id,
            AbbreviatedName = dataModel.AbbreviatedName,
            Name = dataModel.Name
        };

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

            var now = this.DatetimeProvider.Now;
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

            var now = this.DatetimeProvider.Now;
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
