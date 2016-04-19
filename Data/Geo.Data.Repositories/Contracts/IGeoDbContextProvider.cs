namespace ProcessingTools.Geo.Data.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Entity.Contracts;
    using ProcessingTools.Geo.Data;

    public interface IGeoDbContextProvider : IDbContextProvider<GeoDbContext>
    {
    }
}