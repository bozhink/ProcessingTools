namespace ProcessingTools.Journals.Services.Data.Contracts.Models
{
    using ProcessingTools.Models.Contracts;

    public interface IPublisherDetails : IPublisher, IAddressable, IModelWithUserInformation, IDetailedModel, IServiceModel
    {
    }
}
