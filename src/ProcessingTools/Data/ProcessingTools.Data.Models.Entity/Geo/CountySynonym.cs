namespace ProcessingTools.Data.Models.Entity.Geo
{
    public class CountySynonym : Synonym
    {
        public virtual int CountyId { get; set; }

        public virtual County County { get; set; }
    }
}
