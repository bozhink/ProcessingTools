namespace ProcessingTools.Data.Models.Entity.Geo
{
    using ProcessingTools.Models.Contracts;

    public class CountrySynonym : Synonym, IDataModel
    {
        public virtual int CountryId { get; set; }

        public virtual Country Country { get; set; }
    }
}
