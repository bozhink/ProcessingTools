namespace ProcessingTools.Data.Entity.Geo
{
    using ProcessingTools.Data.Entity.Abstractions;

    public class GeoDataRepository<T> : EntityRepository<GeoDbContext, T>, IGeoDataRepository<T>
        where T : class
    {
        public GeoDataRepository(GeoDbContext context)
            : base(context)
        {
        }
    }
}
