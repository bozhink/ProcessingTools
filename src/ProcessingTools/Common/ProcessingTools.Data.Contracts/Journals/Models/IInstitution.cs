namespace ProcessingTools.Contracts.Data.Journals.Models
{
    using ProcessingTools.Models.Contracts;

    public interface IInstitution : IAddressable, IAbbreviatedNameableStringIdentifiable, IModelWithUserInformation
    {
    }
}
