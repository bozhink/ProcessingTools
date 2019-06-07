namespace ProcessingTools.Data.Models.Entity.Geo
{
    using ProcessingTools.Contracts.Models;

    public class CountrySynonym : Synonym, IDataModel
    {
        public virtual int CountryId { get; set; }

        public virtual Country Country { get; set; }
    }
}
