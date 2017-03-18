namespace ProcessingTools.Journals.Services.Data.Contracts.Models
{
    using ProcessingTools.Contracts.Models;

    public interface IInstitutionDetails : IInstitution, IAddressable, IModelWithUserInformation, IDetailedModel, IServiceModel
    {
    }
}
