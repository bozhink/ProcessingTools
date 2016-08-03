namespace ProcessingTools.Geo.Services.Data.Models
{
    using System.Collections.Generic;

    public class ContinentServiceModel
    {
        public ContinentServiceModel()
        {
            this.Synonyms = new HashSet<ContinentSynonymServiceModel>();
            this.Countries = new HashSet<CountryServiceModel>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<ContinentSynonymServiceModel> Synonyms { get; set; }

        public ICollection<CountryServiceModel> Countries { get; set; }
    }
}
