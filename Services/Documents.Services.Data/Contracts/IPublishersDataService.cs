namespace ProcessingTools.Documents.Services.Data.Contracts
{
    using Models.Publishers;
    using ProcessingTools.Services.Common.Contracts;

    public interface IPublishersDataService : IMvcDataService<PublisherMinimalServiceModel, PublisherServiceModel, PublisherDetailsServiceModel>
    {
    }
}
