namespace ProcessingTools.Bio.Environments.Data.Contracts
{
    using System.Data.Entity.Infrastructure;

    public interface IBioEnvironmentsDbContextFactory : IDbContextFactory<BioEnvironmentsDbContext>
    {
        string ConnectionString { get; set; }
    }
}