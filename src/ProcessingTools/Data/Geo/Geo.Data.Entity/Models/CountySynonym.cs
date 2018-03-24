namespace ProcessingTools.Geo.Data.Entity.Models
{
    using ProcessingTools.Models.Contracts;

    public class CountySynonym : Synonym, IDataModel
    {
        public virtual int CountyId { get; set; }

        public virtual County County { get; set; }
    }
}
