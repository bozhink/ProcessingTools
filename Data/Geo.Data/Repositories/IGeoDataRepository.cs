namespace ProcessingTools.Geo.Data.Repositories
{
    using ProcessingTools.Data.Common.Repositories;

    public interface IGeoDataRepository<T> : IRepository<T>
        where T : class
    {
    }
}