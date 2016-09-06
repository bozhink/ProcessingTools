namespace ProcessingTools.Geo.Data.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public interface IGeoDataRepositoryProvider<T> : IGenericRepositoryProvider<T>
        where T : class
    {
    }
}
