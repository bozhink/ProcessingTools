namespace ProcessingTools.Documents.Services.Data.Models.Publishers.Contracts
{
    using ProcessingTools.Contracts;
    using ProcessingTools.Services.Common.Models.Contracts;

    public interface IPublisherSimpleServiceModel : ISimpleServiceModel, IPublisherUpdatableServiceModel, IModelWithUserInformation
    {
    }
}
