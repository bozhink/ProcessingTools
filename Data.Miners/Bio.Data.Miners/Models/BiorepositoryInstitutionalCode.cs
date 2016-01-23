namespace ProcessingTools.Bio.Data.Miners.Models
{
    using Contracts;

    public class BiorepositoryInstitutionalCode : IBiorepositoryInstitutionalCode
    {
        public string Description { get; set; }

        public string InstitutionalCode { get; set; }

        public string Url { get; set; }
    }
}