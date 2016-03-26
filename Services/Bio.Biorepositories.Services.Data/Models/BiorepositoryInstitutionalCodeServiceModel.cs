namespace ProcessingTools.Bio.Biorepositories.Services.Data.Models
{
    using Contracts;

    public class BiorepositoryInstitutionalCodeServiceModel : IBiorepositoryInstitutionalCodeServiceModel
    {
        public string InstitutionalCode { get; set; }

        public string NameOfInstitution { get; set; }

        public string Url { get; set; }
    }
}