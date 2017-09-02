namespace ProcessingTools.Contracts.Data.Journals.Models
{
    using ProcessingTools.Contracts.Models;

    public interface IPublisher : IAddressable, IAbbreviatedNameableStringIdentifiable, IModelWithUserInformation
    {
    }
}
