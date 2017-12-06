namespace ProcessingTools.Geo.Data.Entity.Models
{
    using ProcessingTools.Contracts.Models;

    public class CountySynonym : Synonym, IDataModel
    {
        public virtual int CountyId { get; set; }

        public virtual County County { get; set; }
    }
}
