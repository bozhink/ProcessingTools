namespace ProcessingTools.Data.Entity.Geo
{
    using ProcessingTools.Data.Entity.Abstractions;

    public class GeoDataRepository<T> : EntityGenericRepository<GeoDbContext, T>, IGeoDataRepository<T>
        where T : class
    {
        public GeoDataRepository(IDbContextProvider<GeoDbContext> contextProvider)
            : base(contextProvider)
        {
        }
    }
}
