namespace ProcessingTools.Geo.Data.Entity.Contracts
{
    using System.Data.Entity;
    using ProcessingTools.Data.Common.Entity.Contracts;
    using ProcessingTools.Geo.Data.Entity.Models;

    public interface IGeoDbContext : IDbContext
    {
        IDbSet<City> Cities { get; set; }

        IDbSet<CitySynonym> CitySynonyms { get; set; }

        IDbSet<Continent> Continents { get; set; }

        IDbSet<ContinentSynonym> ContinentSynonyms { get; set; }

        IDbSet<County> Counties { get; set; }

        IDbSet<Country> Countries { get; set; }

        IDbSet<CountrySynonym> CountrySynonyms { get; set; }

        IDbSet<CountySynonym> CountySynonyms { get; set; }

        IDbSet<District> District { get; set; }

        IDbSet<DistrictSynonym> DistrictSynonyms { get; set; }

        IDbSet<GeoEpithet> GeoEpithets { get; set; }

        IDbSet<GeoName> GeoNames { get; set; }

        IDbSet<Municipality> Municipalities { get; set; }

        IDbSet<MunicipalitySynonym> MunicipalitySynonyms { get; set; }

        IDbSet<PostCode> PostCodes { get; set; }

        IDbSet<Province> Provinces { get; set; }

        IDbSet<ProvinceSynonym> ProvinceSynonyms { get; set; }

        IDbSet<Region> Regions { get; set; }

        IDbSet<RegionSynonym> RegionSynonyms { get; set; }

        IDbSet<State> States { get; set; }

        IDbSet<StateSynonym> StateSynonyms { get; set; }
    }
}
