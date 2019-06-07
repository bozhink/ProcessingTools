namespace ProcessingTools.Data.Models.Entity.Geo
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Models.Geo;

    public class ContinentModel : IContinent
    {
        public ICollection<ICountry> Countries { get; set; } = new HashSet<ICountry>();

        public int Id { get; set; }

        public string Name { get; set; }

        public string AbbreviatedName { get; set; }

        public ICollection<IContinentSynonym> Synonyms { get; set; } = new HashSet<IContinentSynonym>();
    }
}
