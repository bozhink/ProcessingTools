namespace ProcessingTools.Journals.Services.Data.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Abstractions.Services;
    using Contracts.Models;
    using Contracts.Services;
    using Models.DataModels;
    using Models.ServiceModels;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Common.Expressions;
    using TDataModel = ProcessingTools.Journals.Data.Common.Contracts.Models.IPublisher;
    using TDetailedServiceModel = ProcessingTools.Journals.Services.Data.Contracts.Models.IPublisherDetails;
    using TRepository = ProcessingTools.Journals.Data.Common.Contracts.Repositories.IPublishersRepository;
    using TServiceModel = ProcessingTools.Journals.Services.Data.Contracts.Models.IPublisher;

    public class PublishersDataService : AbstractAddresssableDataService<TServiceModel, TDetailedServiceModel, TDataModel, TRepository>, IPublishersDataService
    {
        public PublishersDataService(TRepository repository, IDateTimeProvider datetimeProvider)
            : base(repository, datetimeProvider)
        {
        }

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

            await this.Repository.Add(dataModel);

            await this.Repository.SaveChanges();

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

            await this.Repository.Update(
                model.Id,
                ExpressionBuilder<TDataModel>
                    .UpdateExpression
                    .Set(p => p.Name, model.Name)
                    .Set(p => p.AbbreviatedName, model.AbbreviatedName)
                    .Set(p => p.ModifiedByUser, user)
                    .Set(p => p.DateModified, now));

            await this.Repository.SaveChanges();

            return model.Id;
        }
    }
}
