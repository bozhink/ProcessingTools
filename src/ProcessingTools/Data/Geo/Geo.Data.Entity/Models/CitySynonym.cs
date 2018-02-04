namespace ProcessingTools.Geo.Data.Entity.Models
{
    using ProcessingTools.Models.Contracts;

    public class CitySynonym : Synonym, IDataModel
    {
        public virtual int CityId { get; set; }

        public virtual City City { get; set; }
    }
}
