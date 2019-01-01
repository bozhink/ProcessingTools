namespace ProcessingTools.Data.Entity.Geo
{
    using ProcessingTools.Data.Entity.Abstractions;

    public interface IGeoDataRepository<T> : IEntityGenericRepository<T>
        where T : class
    {
    }
}
