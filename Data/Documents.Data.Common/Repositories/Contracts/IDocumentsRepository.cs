namespace ProcessingTools.Documents.Data.Common.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Entity.Repositories.Contracts;

    public interface IDocumentsRepository<T> : IEntityGenericRepository<T>
        where T : class
    {
    }
}
