namespace ProcessingTools.MediaType.Data.Contracts
{
    using System.Data.Entity.Infrastructure;

    public interface IMediatypesDbContextFactory : IDbContextFactory<MediaTypesDbContext>
    {
        string ConnectionString { get; set; }
    }
}