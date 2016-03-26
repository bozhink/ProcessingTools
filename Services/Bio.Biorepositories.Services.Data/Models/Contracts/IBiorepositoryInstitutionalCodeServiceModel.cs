namespace ProcessingTools.Bio.Biorepositories.Services.Data.Models.Contracts
{
    public interface IBiorepositoryInstitutionalCodeServiceModel
    {
        string InstitutionalCode { get; set; }

        string NameOfInstitution { get; set; }

        string Url { get; set; }
    }
}
