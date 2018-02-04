namespace ProcessingTools.Geo.Data.Entity.Models
{
    using ProcessingTools.Models.Contracts;

    public class DistrictSynonym : Synonym, IDataModel
    {
        public virtual int DistrictId { get; set; }

        public virtual District District { get; set; }
    }
}
