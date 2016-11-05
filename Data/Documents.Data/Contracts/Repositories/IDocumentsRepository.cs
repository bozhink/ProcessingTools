namespace ProcessingTools.Documents.Data.Contracts.Repositories
{
    using ProcessingTools.Data.Common.Entity.Repositories.Contracts;

    public interface IDocumentsRepository<T> : IEntityGenericRepository<T>
        where T : class
    {
    }
}
