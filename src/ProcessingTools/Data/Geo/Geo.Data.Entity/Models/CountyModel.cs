namespace ProcessingTools.Geo.Data.Entity.Models
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Models.Geo;

    internal class CountyModel : ICounty
    {
        public ICollection<ICity> Cities { get; set; } = new HashSet<ICity>();

        public int CountryId { get; set; }

        public int? DistrictId { get; set; }

        public int Id { get; set; }

        public int? MunicipalityId { get; set; }

        public string Name { get; set; }

        public string AbbreviatedName { get; set; }

        public int? ProvinceId { get; set; }

        public int? RegionId { get; set; }

        public int? StateId { get; set; }

        public ICollection<ICountySynonym> Synonyms { get; set; } = new HashSet<ICountySynonym>();
    }
}
