namespace ProcessingTools.Geo.Data.Entity.Models
{
    using ProcessingTools.Models.Contracts;

    public class RegionSynonym : Synonym, IDataModel
    {
        public virtual int RegionId { get; set; }

        public virtual Region Region { get; set; }
    }
}
