namespace ProcessingTools.Journals.Data.Common.Contracts.Models
{
    using ProcessingTools.Contracts.Models;

    public interface IPublisher : IAddressable, IAbbreviatedNameableGuidIdentifiable, IModelWithUserInformation
    {
    }
}
