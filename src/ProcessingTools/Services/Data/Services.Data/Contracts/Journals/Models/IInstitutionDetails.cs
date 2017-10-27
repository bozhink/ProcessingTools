namespace ProcessingTools.Journals.Services.Data.Contracts.Models
{
    using ProcessingTools.Models.Contracts;

    public interface IInstitutionDetails : IInstitution, IAddressable, ICreated, IModified, IDetailedModel, IServiceModel
    {
    }
}
