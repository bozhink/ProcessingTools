using ProcessingTools.Data.Miners.Contracts.Models.Bio;

namespace ProcessingTools.Data.Miners.Models.Bio
{
    public class BiorepositoriesInstitution : IBiorepositoriesInstitution
    {
        public string InstitutionalCode { get; set; }

        public string NameOfInstitution { get; set; }

        public string Url { get; set; }
    }
}
