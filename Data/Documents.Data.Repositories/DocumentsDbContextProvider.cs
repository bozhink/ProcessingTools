namespace ProcessingTools.Documents.Data.Repositories
{
    using Contracts;

    public class DocumentsDbContextProvider : IDocumentsDbContextProvider
    {
        public DocumentsDbContext Create()
        {
            return DocumentsDbContext.Create();
        }
    }
}
