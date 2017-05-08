namespace ProcessingTools.Geo.Services.Data.Entity.Models
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Services.Data.Geo.Models;

    internal class Country : ICountry
    {
        public string CallingCode { get; set; }

        public ICollection<ICity> Cities { get; set; } = new HashSet<ICity>();

        public ICollection<IContinent> Continents { get; set; } = new HashSet<IContinent>();

        public ICollection<ICounty> Counties { get; set; } = new HashSet<ICounty>();

        public ICollection<IDistrict> Districts { get; set; } = new HashSet<IDistrict>();

        public int Id { get; set; }

        public string Iso639xCode { get; set; }

        public string LanguageCode { get; set; }

        public ICollection<IMunicipality> Municipalities { get; set; } = new HashSet<IMunicipality>();

        public string Name { get; set; }

        public string AbbreviatedName { get; set; }

        public ICollection<IProvince> Provinces { get; set; } = new HashSet<IProvince>();

        public ICollection<IRegion> Regions { get; set; } = new HashSet<IRegion>();

        public ICollection<IState> States { get; set; } = new HashSet<IState>();

        public ICollection<ICountrySynonym> Synonyms { get; set; } = new HashSet<ICountrySynonym>();
    }
}
