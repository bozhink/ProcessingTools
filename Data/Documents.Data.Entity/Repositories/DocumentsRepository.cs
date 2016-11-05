namespace ProcessingTools.Documents.Data.Repositories
{
    using Contracts;
    using Contracts.Repositories;
    using ProcessingTools.Data.Common.Entity.Models.Contracts;
    using ProcessingTools.Data.Common.Entity.Repositories;

    public class DocumentsRepository<T> : EntityPreJoinedGenericRepository<DocumentsDbContext, T>, IDocumentsRepository<T>
        where T : class, IEntityWithPreJoinedFields
    {
        public DocumentsRepository(IDocumentsDbContextProvider contextProvider)
            : base(contextProvider)
        {
        }
    }
}
