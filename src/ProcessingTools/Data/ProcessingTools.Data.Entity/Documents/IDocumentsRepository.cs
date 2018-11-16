namespace ProcessingTools.Data.Entity.Documents
{
    using ProcessingTools.Data.Entity.Abstractions;

    public interface IDocumentsRepository<T> : IEntityGenericRepository<T>
        where T : class
    {
    }
}
