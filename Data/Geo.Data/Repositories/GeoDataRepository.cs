namespace ProcessingTools.Geo.Data.Repositories
{
    using ProcessingTools.Data.Common.Entity.Repositories;
    using ProcessingTools.Geo.Data.Contracts;
    using ProcessingTools.Geo.Data.Repositories.Contracts;

    public class GeoDataRepository<T> : EfGenericRepository<GeoDbContext, T>, IGeoDataRepository<T>
        where T : class
    {
        public GeoDataRepository(IGeoDbContextProvider contextProvider)
            : base(contextProvider)
        {
        }
    }
}
