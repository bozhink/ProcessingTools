namespace ProcessingTools.Documents.Data.Entity.Contracts
{
    using ProcessingTools.Data.Contracts;

    public interface IDocumentsRepositoryProvider<T> : ICrudRepositoryProvider<T>
        where T : class
    {
    }
}
