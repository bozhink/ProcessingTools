namespace ProcessingTools.Bio.Data.Contracts
{
    using System.Data.Entity.Infrastructure;

    public interface IBioDbContextFactory : IDbContextFactory<BioDbContext>
    {
        string ConnectionString { get; set; }
    }
}
