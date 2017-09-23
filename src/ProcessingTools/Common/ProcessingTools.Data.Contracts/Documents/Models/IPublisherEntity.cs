namespace ProcessingTools.Contracts.Data.Documents.Models
{
    using ProcessingTools.Models.Contracts;

    public interface IPublisherEntity : IAddressableEntity, IAbbreviatedNameableGuidIdentifiable, IModelWithUserInformation
    {
    }
}
