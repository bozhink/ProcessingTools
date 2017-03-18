namespace ProcessingTools.Journals.Services.Data.Contracts.Models
{
    using ProcessingTools.Contracts.Models;

    public interface IPublisherDetails : IPublisher, IAddressable, IModelWithUserInformation, IDetailedModel
    {
    }
}
