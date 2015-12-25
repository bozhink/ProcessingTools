namespace ProcessingTools.Geo.Data.Repositories
{
    using Contracts;
    using ProcessingTools.Data.Common.Repositories;

    public class EfGeoDataGenericRepository<T> : EfGenericRepository<IGeoDbContext, T>, IGeoDataRepository<T>
        where T : class
    {
        public EfGeoDataGenericRepository(IGeoDbContext context)
            : base(context)
        {
        }
    }
}
