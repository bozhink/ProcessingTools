namespace ProcessingTools.Documents.Data.Common
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