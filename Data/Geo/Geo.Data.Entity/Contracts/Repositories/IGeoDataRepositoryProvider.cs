namespace ProcessingTools.Geo.Data.Entity.Contracts.Repositories
{
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IGeoDataRepositoryProvider<T> : ICrudRepositoryProvider<T>
        where T : class
    {
    }
}
