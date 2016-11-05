namespace ProcessingTools.Documents.Data.Entity.Contracts
{
    using System.Data.Entity.Infrastructure;

    public interface IDocumentsDbContextFactory : IDbContextFactory<DocumentsDbContext>
    {
        string ConnectionString { get; set; }
    }
}