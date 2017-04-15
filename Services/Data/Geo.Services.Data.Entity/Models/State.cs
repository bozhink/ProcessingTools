namespace ProcessingTools.Geo.Services.Data.Entity.Models
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Services.Data.Geo.Models;

    internal class State : IState
    {
        public ICollection<ICity> Cities { get; set; } = new HashSet<ICity>();

        public ICollection<ICounty> Counties { get; set; } = new HashSet<ICounty>();

        public int CountryId { get; set; }

        public ICollection<IDistrict> Districts { get; set; } = new HashSet<IDistrict>();

        public int Id { get; set; }

        public ICollection<IMunicipality> Municipalities { get; set; } = new HashSet<IMunicipality>();

        public string Name { get; set; }

        public ICollection<IProvince> Provinces { get; set; } = new HashSet<IProvince>();

        public ICollection<IRegion> Regions { get; set; } = new HashSet<IRegion>();

        public ICollection<IStateSynonym> Synonyms { get; set; } = new HashSet<IStateSynonym>();
    }
}
