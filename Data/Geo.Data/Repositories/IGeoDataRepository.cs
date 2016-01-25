namespace ProcessingTools.Geo.Data.Repositories
{
    using ProcessingTools.Data.Common.Repositories;

    public interface IGeoDataRepository<T> : IEfRepository<T>
        where T : class
    {
    }
}