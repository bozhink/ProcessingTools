namespace ProcessingTools.Data.Models.Entity.Geo
{
    public class CitySynonym : Synonym
    {
        public virtual int CityId { get; set; }

        public virtual City City { get; set; }
    }
}
