namespace ProcessingTools.Contracts.Data.Journals.Models
{
    using ProcessingTools.Contracts.Models;

    public interface IInstitution : IAddressable, IAbbreviatedNameableStringIdentifiable, IModelWithUserInformation
    {
    }
}
