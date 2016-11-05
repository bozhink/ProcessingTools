namespace ProcessingTools.Documents.Data.Contracts
{
    using System.Data.Entity.Infrastructure;

    public interface IDocumentsDbContextFactory : IDbContextFactory<DocumentsDbContext>
    {
        string ConnectionString { get; set; }
    }
}