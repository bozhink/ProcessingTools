namespace ProcessingTools.MediaType.Data.Repositories.Contracts
{
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IMediaTypesRepositoryProvider<T> : ISearchableCountableCrudRepositoryProvider<T>
        where T : class
    {
    }
}
