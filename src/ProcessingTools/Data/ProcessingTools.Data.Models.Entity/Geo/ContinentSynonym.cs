namespace ProcessingTools.Data.Models.Entity.Geo
{
    using ProcessingTools.Models.Contracts;

    public class ContinentSynonym : Synonym, IDataModel
    {
        public virtual int ContinentId { get; set; }

        public virtual Continent Continent { get; set; }
    }
}
