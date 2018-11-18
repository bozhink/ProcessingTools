namespace ProcessingTools.Data.Models.Entity.Geo
{
    using ProcessingTools.Models.Contracts;

    public class RegionSynonym : Synonym, IDataModel
    {
        public virtual int RegionId { get; set; }

        public virtual Region Region { get; set; }
    }
}
