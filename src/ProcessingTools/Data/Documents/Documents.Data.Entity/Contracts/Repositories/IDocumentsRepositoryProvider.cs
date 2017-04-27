namespace ProcessingTools.Documents.Data.Entity.Contracts.Repositories
{
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IDocumentsRepositoryProvider<T> : ICrudRepositoryProvider<T>
        where T : class
    {
    }
}
