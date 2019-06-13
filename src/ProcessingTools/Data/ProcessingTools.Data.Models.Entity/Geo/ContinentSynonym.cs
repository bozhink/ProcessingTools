namespace ProcessingTools.Data.Models.Entity.Geo
{
    public class ContinentSynonym : Synonym
    {
        public virtual int ContinentId { get; set; }

        public virtual Continent Continent { get; set; }
    }
}
