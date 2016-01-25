namespace ProcessingTools.Geo.Data.Repositories
{
    using ProcessingTools.Data.Common.Repositories;
    using ProcessingTools.Geo.Data.Contracts;
    using ProcessingTools.Geo.Data.Repositories.Contracts;

    public class GeoDataRepository<T> : EfGenericRepository<IGeoDbContext, T>, IGeoDataRepository<T>
        where T : class
    {
        public GeoDataRepository(IGeoDbContext context)
            : base(context)
        {
        }
    }
}
