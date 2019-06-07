namespace ProcessingTools.Data.Models.Entity.Geo
{
    using ProcessingTools.Contracts.Models;

    public class RegionSynonym : Synonym, IDataModel
    {
        public virtual int RegionId { get; set; }

        public virtual Region Region { get; set; }
    }
}
