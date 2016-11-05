namespace ProcessingTools.Geo.Data.Entity.Contracts
{
    using System.Data.Entity;
    using Models;
    using ProcessingTools.Data.Common.Entity.Contracts;

    public interface IGeoDbContext : IDbContext
    {
        IDbSet<City> Cities { get; set; }

        IDbSet<Continent> Continents { get; set; }

        IDbSet<Country> Countries { get; set; }

        IDbSet<GeoEpithet> GeoEpithets { get; set; }

        IDbSet<GeoName> GeoNames { get; set; }

        IDbSet<PostCode> PostCodes { get; set; }

        IDbSet<State> States { get; set; }
    }
}
