namespace ProcessingTools.Mediatypes.Data.Entity.Contracts
{
    using System.Data.Entity.Infrastructure;

    public interface IMediatypesDbContextFactory : IDbContextFactory<MediatypesDbContext>
    {
        string ConnectionString { get; set; }
    }
}
