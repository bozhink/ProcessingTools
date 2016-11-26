namespace ProcessingTools.Geo.Data.Entity.Repositories
{
    using Contracts;
    using Contracts.Repositories;
    using ProcessingTools.Data.Common.Entity.Repositories;

    public class GeoDataRepository<T> : EntityGenericRepository<GeoDbContext, T>, IGeoDataRepository<T>
        where T : class
    {
        public GeoDataRepository(IGeoDbContextProvider contextProvider)
            : base(contextProvider)
        {
        }
    }
}
