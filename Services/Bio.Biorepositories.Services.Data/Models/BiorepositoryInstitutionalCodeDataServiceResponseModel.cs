namespace ProcessingTools.Bio.Biorepositories.Services.Data.Models
{
    using Contracts;

    public class BiorepositoryInstitutionalCodeDataServiceResponseModel : IBiorepositoryInstitutionalCode
    {
        public string InstitutionalCode { get; set; }

        public string NameOfInstitution { get; set; }

        public string Url { get; set; }
    }
}