namespace ProcessingTools.Documents.Data.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Entity.Contracts;

    public interface IDocumentsDbContextProvider : IDbContextProvider<DocumentsDbContext>
    {
    }
}
