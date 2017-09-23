namespace ProcessingTools.Contracts.Data.Journals.Models
{
    using ProcessingTools.Models.Contracts;

    public interface IPublisher : IAddressable, IAbbreviatedNameableStringIdentifiable, IModelWithUserInformation
    {
    }
}
