namespace ProcessingTools.Data.Models.Entity.Geo
{
    using ProcessingTools.Contracts.Models;

    public class DistrictSynonym : Synonym
    {
        public virtual int DistrictId { get; set; }

        public virtual District District { get; set; }
    }
}
