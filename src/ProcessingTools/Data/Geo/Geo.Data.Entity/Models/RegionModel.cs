namespace ProcessingTools.Geo.Data.Entity.Models
{
    using System.Collections.Generic;
    using ProcessingTools.Models.Contracts.Geo;

    internal class RegionModel : IRegion
    {
        public ICollection<ICity> Cities { get; set; } = new HashSet<ICity>();

        public ICollection<ICounty> Counties { get; set; } = new HashSet<ICounty>();

        public int CountryId { get; set; }

        public ICollection<IDistrict> Districts { get; set; } = new HashSet<IDistrict>();

        public int Id { get; set; }

        public ICollection<IMunicipality> Municipalities { get; set; } = new HashSet<IMunicipality>();

        public string Name { get; set; }

        public string AbbreviatedName { get; set; }

        public int? ProvinceId { get; set; }

        public int? StateId { get; set; }

        public ICollection<IRegionSynonym> Synonyms { get; set; } = new HashSet<IRegionSynonym>();
    }
}
