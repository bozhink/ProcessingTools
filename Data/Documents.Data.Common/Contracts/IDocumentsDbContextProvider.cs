namespace ProcessingTools.Documents.Data.Common.Contracts
{
    using ProcessingTools.Data.Common.Entity.Contracts;

    public interface IDocumentsDbContextProvider : IDbContextProvider<DocumentsDbContext>
    {
    }
}