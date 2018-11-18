namespace ProcessingTools.Data.Entity.Geo
{
    using ProcessingTools.Data.Entity.Abstractions;

    public interface IGeoRepository<T> : IEfRepository<GeoDbContext, T>
        where T : class
    {
    }
}
