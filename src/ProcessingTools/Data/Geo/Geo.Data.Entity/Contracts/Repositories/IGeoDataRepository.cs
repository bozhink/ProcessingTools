namespace ProcessingTools.Geo.Data.Entity.Contracts.Repositories
{
    using ProcessingTools.Data.Common.Entity.Contracts;

    public interface IGeoDataRepository<T> : IEntityGenericRepository<T>
        where T : class
    {
    }
}
