namespace ProcessingTools.Geo.Data.Entity.Models
{
    using ProcessingTools.Contracts.Models;

    public class CountrySynonym : Synonym, IDataModel
    {
        public virtual int CountryId { get; set; }

        public virtual Country Country { get; set; }
    }
}
