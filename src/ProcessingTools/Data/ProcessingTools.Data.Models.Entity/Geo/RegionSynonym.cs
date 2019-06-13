namespace ProcessingTools.Data.Models.Entity.Geo
{
    public class RegionSynonym : Synonym
    {
        public virtual int RegionId { get; set; }

        public virtual Region Region { get; set; }
    }
}
