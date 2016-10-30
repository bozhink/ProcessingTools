namespace ProcessingTools.Documents.Data.Repositories.Contracts
{
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IDocumentsRepositoryProvider<T> : IGenericRepositoryProvider<T>
        where T : class
    {
    }
}
