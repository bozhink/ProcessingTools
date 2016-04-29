namespace ProcessingTools.Documents.Data
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
