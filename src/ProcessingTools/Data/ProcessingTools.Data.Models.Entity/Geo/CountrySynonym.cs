namespace ProcessingTools.Data.Models.Entity.Geo
{
    public class CountrySynonym : Synonym
    {
        public virtual int CountryId { get; set; }

        public virtual Country Country { get; set; }
    }
}
