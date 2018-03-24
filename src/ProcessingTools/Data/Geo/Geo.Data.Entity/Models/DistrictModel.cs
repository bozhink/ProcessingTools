namespace ProcessingTools.Geo.Data.Entity.Models
{
    using System.Collections.Generic;
    using ProcessingTools.Models.Contracts.Geo;

    internal class DistrictModel : IDistrict
    {
        public ICollection<ICity> Cities { get; set; } = new HashSet<ICity>();

        public ICollection<ICounty> Counties { get; set; } = new HashSet<ICounty>();

        public int CountryId { get; set; }

        public int Id { get; set; }

        public ICollection<IMunicipality> Municipalities { get; set; } = new HashSet<IMunicipality>();

        public string Name { get; set; }

        public string AbbreviatedName { get; set; }

        public int? ProvinceId { get; set; }

        public int? RegionId { get; set; }

        public int? StateId { get; set; }

        public ICollection<IDistrictSynonym> Synonyms { get; set; } = new HashSet<IDistrictSynonym>();
    }
}
