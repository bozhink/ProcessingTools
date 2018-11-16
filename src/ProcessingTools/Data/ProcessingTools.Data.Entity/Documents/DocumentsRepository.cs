namespace ProcessingTools.Data.Entity.Documents
{
    using ProcessingTools.Data.Entity.Abstractions;

    public class DocumentsRepository<T> : EntityGenericRepository<DocumentsDbContext, T>, IDocumentsRepository<T>
        where T : class
    {
        public DocumentsRepository(IDbContextProvider<DocumentsDbContext> contextProvider)
            : base(contextProvider)
        {
        }
    }
}
