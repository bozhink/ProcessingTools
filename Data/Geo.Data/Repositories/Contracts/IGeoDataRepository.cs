namespace ProcessingTools.Geo.Data.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Entity.Repositories.Contracts;

    public interface IGeoDataRepository<T> : IEntityGenericRepository<T>
        where T : class
    {
    }
}