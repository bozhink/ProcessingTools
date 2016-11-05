namespace ProcessingTools.Geo.Data.Contracts
{
    using ProcessingTools.Data.Common.Entity.Contracts;

    public interface IGeoDbContextProvider : IDbContextProvider<GeoDbContext>
    {
    }
}