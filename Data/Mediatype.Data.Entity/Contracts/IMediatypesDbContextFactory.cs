namespace ProcessingTools.MediaType.Data.Entity.Contracts
{
    using System.Data.Entity.Infrastructure;

    public interface IMediatypesDbContextFactory : IDbContextFactory<MediaTypesDbContext>
    {
        string ConnectionString { get; set; }
    }
}
