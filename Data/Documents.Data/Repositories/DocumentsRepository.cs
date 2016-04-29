namespace ProcessingTools.Documents.Data.Repositories
{
    using ProcessingTools.Data.Common.Entity.Repositories;
    using ProcessingTools.Documents.Data.Contracts;
    using ProcessingTools.Documents.Data.Repositories.Contracts;

    public class DocumentsRepository<T> : EntityGenericRepository<DocumentsDbContext, T>, IDocumentsRepository<T>
        where T : class
    {
        public DocumentsRepository(IDocumentsDbContextProvider contextProvider)
            : base(contextProvider)
        {
        }
    }
}