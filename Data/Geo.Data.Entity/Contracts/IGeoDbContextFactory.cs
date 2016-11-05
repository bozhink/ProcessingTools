namespace ProcessingTools.Geo.Data.Contracts
{
    using System.Data.Entity.Infrastructure;

    public interface IGeoDbContextFactory : IDbContextFactory<GeoDbContext>
    {
        string ConnectionString { get; set; }
    }
}