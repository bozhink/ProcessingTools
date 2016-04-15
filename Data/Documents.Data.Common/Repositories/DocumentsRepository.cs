namespace ProcessingTools.Documents.Data.Common.Repositories
{
    using Contracts;
    using ProcessingTools.Data.Common.Entity.Repositories;
    using ProcessingTools.Documents.Data;
    using ProcessingTools.Documents.Data.Common.Contracts;

    public class DocumentsRepository<T> : EntityGenericRepository<DocumentsDbContext, T>, IDocumentsRepository<T>
        where T : class
    {
        public DocumentsRepository(IDocumentsDbContextProvider contextProvider)
            : base(contextProvider)
        {
        }
    }
}