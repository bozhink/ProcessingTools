namespace ProcessingTools.Data.Entity.Geo
{
    using ProcessingTools.Data.Entity.Abstractions;

    public class GeoRepository<T> : EfRepository<GeoDbContext, T>, IGeoRepository<T>
        where T : class
    {
        public GeoRepository(GeoDbContext context)
            : base(context)
        {
        }
    }
}
