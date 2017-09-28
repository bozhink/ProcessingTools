namespace ProcessingTools.Documents.Data.Entity.Contracts.Repositories
{
    using ProcessingTools.Data.Contracts.Repositories;

    public interface IDocumentsRepositoryProvider<T> : ICrudRepositoryProvider<T>
        where T : class
    {
    }
}
