namespace ProcessingTools.Data.Miners.Models
{
    using Contracts.Models;

    public class BiorepositoriesInstitution : IBiorepositoriesInstitution
    {
        public string InstitutionalCode { get; set; }

        public string NameOfInstitution { get; set; }

        public string Url { get; set; }
    }
}
