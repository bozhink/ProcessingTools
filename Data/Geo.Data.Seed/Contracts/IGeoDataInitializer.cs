namespace ProcessingTools.Geo.Data.Seed.Contracts
{
    using ProcessingTools.Data.Common.Entity.Contracts;

    public interface IGeoDataInitializer : IDbContextInitializer<GeoDbContext>
    {
    }
}