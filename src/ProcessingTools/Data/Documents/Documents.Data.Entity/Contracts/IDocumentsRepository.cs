namespace ProcessingTools.Documents.Data.Entity.Contracts
{
    using ProcessingTools.Data.Common.Entity.Contracts;

    public interface IDocumentsRepository<T> : IEntityGenericRepository<T>
        where T : class
    {
    }
}
