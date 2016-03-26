namespace ProcessingTools.Bio.Biorepositories.Services.Data.Models
{
    using Contracts;

    public class BiorepositoryInstitutionServiceModel : IBiorepositoryInstitutionServiceModel
    {
        public string Name { get; set; }

        public string Url { get; set; }
    }
}