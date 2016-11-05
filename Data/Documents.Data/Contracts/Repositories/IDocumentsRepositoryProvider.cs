namespace ProcessingTools.Documents.Data.Contracts.Repositories
{
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IDocumentsRepositoryProvider<T> : ISearchableCountableCrudRepositoryProvider<T>
        where T : class
    {
    }
}
