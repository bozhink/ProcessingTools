namespace ProcessingTools.Geo.Data.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Contracts;
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public interface IGeoDataRepositoryProvider<T> : IGenericRepositoryProvider<IGenericRepository<T>, T>
        where T : class
    {
    }
}