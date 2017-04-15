namespace ProcessingTools.Geo.Data.Entity.Repositories
{
    using ProcessingTools.Data.Common.Entity.Repositories;
    using ProcessingTools.Geo.Data.Entity.Contracts;

    public class GeoRepository<T> : GenericRepository<IGeoDbContext, T>, IGeoRepository<T>
        where T : class
    {
        public GeoRepository(IGeoDbContext context)
            : base(context)
        {
        }
    }
}
