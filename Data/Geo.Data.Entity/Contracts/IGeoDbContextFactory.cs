namespace ProcessingTools.Geo.Data.Entity.Contracts
{
    using System.Data.Entity.Infrastructure;

    public interface IGeoDbContextFactory : IDbContextFactory<GeoDbContext>
    {
        string ConnectionString { get; set; }
    }
}
