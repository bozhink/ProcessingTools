namespace ProcessingTools.Documents.Data.Common.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public interface IDocumentsRepository<T> : IGenericRepository<T>
        where T : class
    {
    }
}