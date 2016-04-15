namespace ProcessingTools.Documents.Data.Repositories
{
    using Contracts;
    using ProcessingTools.Data.Common.Entity.Repositories;

    public class DocumentsRepository<T> : EntityGenericRepository<DocumentsDbContext, T>, IDocumentsRepository<T>
        where T : class
    {
        public DocumentsRepository(IDocumentsDbContextProvider contextProvider)
            : base(contextProvider)
        {
        }
    }
}